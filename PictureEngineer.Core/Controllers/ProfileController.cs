using Firebase.Storage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PictureEngineer.Reponsitories.Firebase;
using PictureEngineer.Controllers;
using PictureEngineer.Data;
using PictureEngineer.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using PictureEngineer.Data.DTO;

namespace PictureEngineer.Core.Controllers.Admin
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly IAppContext _appContext;

        public ProfileController(UserManager<AspNetUser> userManager,
            IHostingEnvironment environment, IConfiguration configuration, IAppContext appContext)
        {
            _userManager = userManager;
            _environment = environment;
            _configuration = configuration;
            _appContext = appContext;
        }

        [Route("profile")]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(user);
        }

        [Route("profile")]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileDto model, string userId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    var user = await _userManager.FindByIdAsync(userId);
                    if(user == null)
                    {
                        return BadRequest("Tài khoản không tồn tại.");
                    }

                    user.FullName = model.FullName;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;

                    var result = await _userManager.UpdateAsync(user);

                    var isLogin = await _appContext.UpdateUserClaim(User, model.FullName);

                    return Ok(user);

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

        [Route("profile/password")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ProfilePassword password, string userId)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    bool checkPassword = await _userManager.CheckPasswordAsync(user, password.PasswordOld);
                    if (!checkPassword) return BadRequest("Mật khẩu cũ không đúng.");

                    await _userManager.ChangePasswordAsync(user, password.PasswordOld, password.PasswordNew);
                    return Ok("Thay đổi mật khẩu thành công. Đăng nhập lại sau 2 giây");
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
