using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Configuration
{
    public static class GlobalozationAndLocalizationConfiguration
    {
        public static void ConfigurateGlolizationAndLocalization(this IServiceCollection services)
        {
            var dtf = new DateTimeFormatInfo
            {
                ShortDatePattern = "MM/dd/yyyy",
            };
            var cultureInfo = new CultureInfo("vi-VN");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            cultureInfo.DateTimeFormat = dtf;

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("vi-VN"){DateTimeFormat=dtf },
                    new CultureInfo("en-US"){DateTimeFormat=dtf },
                    new CultureInfo("ja-JA"){DateTimeFormat=dtf },
                    new CultureInfo("zh-TW"){DateTimeFormat=dtf },
                    new CultureInfo("ko-KR"){DateTimeFormat=dtf },
                    new CultureInfo("la-LA"){DateTimeFormat=dtf },
                    new CultureInfo("ca-CA"){DateTimeFormat=dtf },
                    new CultureInfo("my-MM"){DateTimeFormat=dtf },
                    new CultureInfo("fr-FR"){DateTimeFormat=dtf },
                };
                options.DefaultRequestCulture = new RequestCulture(cultureInfo);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });
        }
    }
}
