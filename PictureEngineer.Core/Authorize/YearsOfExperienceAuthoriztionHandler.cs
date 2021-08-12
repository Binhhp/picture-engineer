using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Authorize
{
    public class YearsOfExperienceRequirement : IAuthorizationRequirement
    {
        public int YearsOfExperienceRequired { get; set; }
        public YearsOfExperienceRequirement(int yearsOfExperienceRequired)
        {
            YearsOfExperienceRequired = yearsOfExperienceRequired;
        }
    }

    public class YearsOfExperienceAuthoriztionHandler : AuthorizationHandler<YearsOfExperienceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, YearsOfExperienceRequirement requirement)
        {
            //Type CareerStarted
            if(!context.User.HasClaim(c => c.Type == "CareerStarted"))
            {
                return Task.CompletedTask;
            }

            var careerStarted = DateTimeOffset.Parse(context.User.FindFirst(c => c.Type == "CareerStarted").Value);

            var yearsOfExperience = Math.Round((DateTimeOffset.Now - careerStarted).TotalDays / 365);

            if(yearsOfExperience >= requirement.YearsOfExperienceRequired)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
