using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PictureEngineer.Core
{
    public interface IAppContext
    {
        Task<bool> CreateUserClaimAsync(string userId, string name, string avatar, IList<string> roles, bool remember);
        Task<bool> UpdateUserClaim(IPrincipal curentPrincipal, string name);
        Task<bool> UpdateUserClaim(IPrincipal curentPrincipal, string name, string avatar);
        string GetClaim(string key);
        string UserId { get; }
        string UserName { get; }
        string RoleName { get; }
        string UserAvt { get; }
    }

    public class AppContext : IAppContext
    {
        private IHttpContextAccessor _httpContextAccessor;
        public AppContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public const string Avatar = "Avatar";
        /// <summary>
        /// Create user claim
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="name">full name user</param>
        /// <param name="avatar">avatar user</param>
        /// <param name="roles">list roles users</param>
        /// <param name="remember">is remember</param>
        /// <returns>Create user claim</returns>
        public async Task<bool> CreateUserClaimAsync(string userId, string name, string avatar, IList<string> roles, bool remember)
        {
            try
            {
                var authenProps = new AuthenticationProperties
                {
                    IsPersistent = remember,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(365))
                };

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
                identity.AddClaim(new Claim(ClaimTypes.Name, name));
                identity.AddClaim(new Claim(Avatar, avatar));
                foreach (var role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                await Current.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Update user claim
        /// </summary>
        /// <param name="curentPrincipal">claim current</param>
        /// <param name="name">name user</param>
        /// <returns>Update user claim</returns>
        public async Task<bool> UpdateUserClaim(IPrincipal curentPrincipal, string name)
        {
            try
            {
                var identity = curentPrincipal.Identity as ClaimsIdentity;
                if (identity == null) return false;

                var existingIdentityName = identity.FindFirst(claim => claim.Type == ClaimTypes.Name);
                if(existingIdentityName != null)
                {
                    identity.RemoveClaim(existingIdentityName);
                    identity.AddClaim(new Claim(ClaimTypes.Name, name));
                    await Current.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserClaim(IPrincipal curentPrincipal, string name, string avatar)
        {
            try
            {
                var identity = curentPrincipal.Identity as ClaimsIdentity;
                if (identity == null) return false;

                var existingIdentityName = identity.FindFirst(claim => claim.Type == ClaimTypes.Name);
                if (existingIdentityName != null)
                {
                    identity.RemoveClaim(existingIdentityName);
                    identity.AddClaim(new Claim(ClaimTypes.Name, name));
                }
                var existingIdentityAvt = identity.FindFirst(claim => claim.Type == Avatar);
                if (existingIdentityAvt != null)
                {
                    identity.RemoveClaim(existingIdentityAvt);
                    identity.AddClaim(new Claim(Avatar, avatar));
                }
                await Current.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return true;

            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Get claim
        /// </summary>
        /// <param name="key">key object</param>
        /// <returns></returns>
        public string GetClaim(string key)
        {
            try
            {
                var result = new List<string>();
                foreach(var claim in Current.User.Claims)
                {
                    if (claim.Type.Equals(key))
                    {
                        result.Add(claim.Value);
                    }
                }
                return string.Join(";", result);
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// Get user name
        /// </summary>
        public string UserName
        {
            get
            {
                return Current.User.Identity.Name;
            }
        }
        /// <summary>
        /// Get user id
        /// </summary>
        public string UserId
        {
            get
            {
                return Current.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }
        /// <summary>
        /// Get role user
        /// </summary>
        public string RoleName
        {
            get
            {
                return Current.User.FindFirst(ClaimTypes.Role).Value;
            }
        }

        /// <summary>
        /// Get Avatar
        /// </summary>
        public string UserAvt
        {
            get
            {
                return Current.User.FindFirst(Avatar).Value;
            }
        }
        public HttpContext Current
        {
            get
            {
                return _httpContextAccessor.HttpContext;
            }
        }
    }
}
