using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PictureEngineer.Data.DTO;
using PictureEngineer.Data.Entities;
using System;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Controllers.Admin
{
    public class AccountLoginController : Controller
    {
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IAppContext _appContext;

        public AccountLoginController(SignInManager<AspNetUser> signInManager,
            UserManager<AspNetUser> userManager, IAppContext appContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appContext = appContext;
        }

        [HttpGet]
        [Route("/admin/login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        /// <summary>
        /// Login account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/admin/login")]
        public async Task<IActionResult> Login(AccountLoginDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email) ?? await _userManager.FindByNameAsync(model.Email);

                    if (user == null)
                    {
                        return BadRequest("Tài khoản không tồn tại.");
                    }

                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.IsLockedOut) return BadRequest("Tài khoản đã bị khóa.");
                    if (result.IsNotAllowed) return BadRequest("Tài khoản không được phép truy cập.");
                    if (result.RequiresTwoFactor) return BadRequest("Authentication 2FA");

                    if (!result.Succeeded)
                    {
                        return BadRequest("Sai tài khoản hoặc mật khẩu.");
                    }
                    else
                    {

                        var isEmailConfirm = await _userManager.IsEmailConfirmedAsync(user);

                        if (!isEmailConfirm)
                        {
                            return BadRequest("Email chưa được xác thực.");
                        }

                        var roles = await _userManager.GetRolesAsync(user);
                        bool isUserClaim = await _appContext.CreateUserClaimAsync(user.Id, user.FullName, user.Picture, roles, true);
                        if (isUserClaim)
                        {
                            return Ok(RedirectToLocal(model.ReturnUrl));
                        }
                        return BadRequest("Error 401 Bad Request.");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/admin/login");
        }

        private string RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return returnUrl;
            else
                return "/admin/dashboard";

        }
    }
}
