using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Authorize
{
    public static class AuthenticationConfiguration
    {
        public static void ConfigurationAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                
            }).AddCookie(options =>
               {
                    //Dia chi tra ve khi khon duoc phep truy cap
                    options.AccessDeniedPath = new PathString("/Unauthorized");

                   options.Cookie = new CookieBuilder
                   {
                        //Domain = "",
                        HttpOnly = true,
                       Name = ".pengineer.Security.Cookie",
                       Path = "/",
                       SameSite = SameSiteMode.Lax,
                       SecurePolicy = CookieSecurePolicy.SameAsRequest
                   };
                   options.Events = new CookieAuthenticationEvents
                   {
                       OnSignedIn = context =>
                       {
                           Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                               "OnSignedIn", context.Principal.Identity.Name);
                           return Task.CompletedTask;
                       },
                       OnSigningOut = context =>
                       {
                           Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                               "OnSigningOut", context.HttpContext.User.Identity.Name);
                           return Task.CompletedTask;
                       },
                       OnValidatePrincipal = context =>
                       {
                           Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                               "OnValidatePrincipal", context.Principal.Identity.Name);
                           return Task.CompletedTask;
                       }
                   };
                   options.LoginPath = new PathString("/admin/Login");
                   options.ReturnUrlParameter = "returnUrl";
               });
        }
    }
}
