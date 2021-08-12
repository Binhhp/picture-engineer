using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using PictureEngineer.Common;
using PictureEngineer.Common.EmailHelper;
using PictureEngineer.Core.Validator;
using PictureEngineer.Data;
using PictureEngineer.Data.DTO;
using PictureEngineer.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Controllers
{
    [Authorize(Policy = RoleSchemes.Admin)]
    public class AccountController : BaseController
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly PictureEngineerDbContext _context;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AspNetUser> userManager, RoleManager<IdentityRole> roleManager,
            PictureEngineerDbContext context, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _emailService = emailService;
        }

        [Route("/admin/accounts")]
        [HttpGet]
        public IActionResult Index()
        {
            var users = _context.User.ToList();
            ViewData["Roles"] = _context.Role.ToList();
            return View(users);
        }
        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        [HttpGet("/admin/accounts/all")]
        public JsonResult JTables()
        {
            var userLogined = _userManager.GetUserId(User);
            var users = _context.User.Where(x => x.Id != userLogined).ToList();
            return Responsed(users);
        }
        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("/admin/accounts")]
        public async Task<IActionResult> DeleteUser([FromQuery]string userId)
        {
            try
            {
                if (userId == _userManager.GetUserId(User)) return BadRequest("Không thể xóa tài khoản đang đăng nhập.");

                if (string.IsNullOrEmpty(userId)) return BadRequest("Không tìm thấy tài khoản.");

                AspNetUser user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return BadRequest("Tài khoản không tồn tại.");
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.ToList());
                }

                return Ok("Xóa tài khoản thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// lock user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost("/admin/LockUser")]
        public async Task<JsonResult> LockUser(string userId, int type)
        {
            try
            {
                if (userId == _userManager.GetUserId(User)) return ResponsedError("Không thể khóa tài khoản đang đăng nhập.");

                if (string.IsNullOrEmpty(userId)) return ResponsedError("Không tìm thấy tài khoản.");

                AspNetUser user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return ResponsedError("Tài khoản không tồn tại.");
                }

                switch (type)
                {
                    case 0:
                        var lockUser = await _userManager.SetLockoutEnabledAsync(user, false);
                        if (!lockUser.Succeeded) return ResponsedError(lockUser.Errors.ToList());

                        return ResponsedSuccess("Đã khóa tài khoản.");
                    case 1:
                        var lockUserHours = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddHours(1));
                        if (!lockUserHours.Succeeded) return ResponsedError(lockUserHours.Errors.ToList());

                        return ResponsedSuccess(string.Format("Đã khóa tài khoản {0} giờ", 1));
                    case 2:
                        var lockUserDay = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddHours(1));
                        if (!lockUserDay.Succeeded) return ResponsedError(lockUserDay.Errors.ToList());

                        return ResponsedSuccess(string.Format("Đã khóa tài khoản {0} ngày", 1));
                    default:
                        break;
                }

                return Json("");
            }
            catch (Exception ex)
            {
                return ResponsedError(ex.Message);
            }
        }
        /// <summary>
        /// unlock user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("/admin/UnlockUser/{userId}")]
        public async Task<JsonResult> UnlockUser(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId)) return ResponsedError("Không tìm thấy tài khoản.");

                AspNetUser user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return ResponsedError("Tài khoản không tồn tại.");
                }

                if (user.LockoutEnabled == false)
                {
                    var result = await _userManager.SetLockoutEnabledAsync(user, true);
                    if (!result.Succeeded) return ResponsedError(result.Errors.ToList());
                }

                if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
                {
                    var unlock = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now);
                    if (!unlock.Succeeded) return ResponsedError(unlock.Errors.ToList());
                }

                return ResponsedSuccess("Đã mở khóa tài khoản.");

            }
            catch (Exception ex)
            {
                return ResponsedError(ex.Message);
            }
        }

        /// <summary>
        /// Register account
        /// </summary>
        /// <param name="model">account</param>
        /// <returns>Register account</returns>
        [Route("/admin/accounts")]
        [HttpPost]
        public async Task<IActionResult> Register(RegistryAccount model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var checkUser = await _userManager.FindByNameAsync(model.UserName);
                    if (checkUser != null) return BadRequest("Tài khoản đã tồn tại.");
                    var checkEmail =  await _userManager.FindByEmailAsync(model.Email);
                    if (checkEmail != null) return BadRequest("Email đã tồn tại.");

                    var role = await _roleManager.FindByIdAsync(model.Role);

                    var user = new AspNetUser
                    {
                        FullName = model.FullName,
                        UserName = model.UserName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Picture = "https://firebasestorage.googleapis.com/v0/b/pengineer-42d51.appspot.com/o/users%2F1596889689_912_Anh-avatar-dep-va-doc-dao-lam-hinh-dai-dien.jpg?alt=media&token=3db2e79f-8701-4ae7-be90-66f7b376735a"
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        if (role != null)
                        {
                            if (!await _userManager.IsInRoleAsync(user, role.Name))
                            {
                                await _userManager.AddToRoleAsync(user, role.NormalizedName);
                            }
                        }


                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        string callbackUrl = Url.Action(
                            action: "ConfirmEmail", controller: "Account", new { userId = user.Id, code = code }, Request.Scheme);

                        var isEmail = await _emailService.SendAsync(new IdentityMessage
                        {
                            Destination = "Đăng ký tài khoản Picture Engineer",
                            Subject = "Đăng ký tài khoản Picture Engineer",
                            Body = $"Xác thực email đăng ký tài khoản tại đây <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>xác nhận email</a>.",
                            EmailAddress = user.Email,
                            NameObject = user.FullName
                        });

                        return Ok("Bạn cần xác nhận email để hoàn thành đăng ký tài khoản.");
                    }
                    else
                    {
                        return BadRequest("Tạo tài khoản thất bại");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Route("/admin/email")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string userId, [FromQuery]string code)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại");
                }
                string token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)); 
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Phiên đã hết hạn.");
                }
                return View();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        [Route("/admin/password")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromQuery]string email)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        return BadRequest("Email không tồn tại.");
                    }

                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    string callbackUrl = Url.Action(
                        action: "ChangeForgotPassword", controller: "Account", new { userId = user.Id, code = code }, Request.Scheme);

                    var isEmail = await _emailService.SendAsync(new IdentityMessage
                    {
                        Destination = "Thay đổi mật khẩu tài khoản Picture Engineer",
                        Subject = "Thay đổi mật khẩu tài khoản Picture Engineer",
                        Body = $"Thay đổi mật khẩu tại đây <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>thay đổi mật khẩu</a>.",
                        EmailAddress = user.Email,
                        NameObject = user.FullName
                    });

                    if (!isEmail.Successed)
                    {
                        return BadRequest();
                    }
                    return Ok("/admin/ForgotPassword");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/admin/ForgotPassword")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        /// <summary>
        /// Change password new
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="code">token change pass</param>
        /// <returns>Change new password</returns>
        [Route("/admin/ChangePassword")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ChangeForgotPassword([FromQuery]string userId, [FromQuery]string code)
        {
            return View(new ChangeForgotPasswordDto { UserId = userId, Code = code });
        }

        [Route("/admin/ChangePassword")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ChangeForgotPassword(ChangeForgotPasswordDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(model.UserId);

                    var isPassword = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (isPassword)
                    {
                        return BadRequest("Mật khẩu đã được đặt. Thay đổi mật khẩu mới.");
                    }
                    if (user == null)
                    {
                       return BadRequest("Tài khoản không tồn tại.");
                    }

                    string token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));
                    var result = await _userManager.ResetPasswordAsync(user, token, model.ConfirmPassword);
                    if (!result.Succeeded)
                    {
                        return BadRequest("Phiên đã hết hạn hoặc không đúng");
                    }
                    return Ok("Thay đổi mật khẩu thành công.");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
