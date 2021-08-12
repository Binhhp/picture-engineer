using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PictureEngineer.Common;
using PictureEngineer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Authorize
{
    public static class PocilyConfiguration
    {
        public static void AddPocilyRoles(this IServiceCollection services)
        {
            services.AddAuthorization(opt =>
            {
                //Role admin
                opt.AddPolicy(RoleSchemes.Admin,
                    builder => builder.RequireRole(RoleSchemes.AdminTarget));

                opt.AddPolicy(RoleSchemes.YearsOfExprience,
                    builder => builder.AddRequirements(
                        new YearsOfExperienceRequirement(30))
                    );
            });

            //Requirements and Hanlder
            services.AddSingleton<IAuthorizationHandler, YearsOfExperienceAuthoriztionHandler>();
        }
    }
}
