using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using PictureEngineer.Data;
using Mailjet.Client;
using AutoMapper;
using PictureEngineer.Common.EmailHelper;
using FluentValidation.AspNetCore;
using PictureEngineer.Core.Validator.Filter;
using PictureEngineer.Core.Configuration;
using PictureEngineer.Core.Authorize;
using PictureEngineer.Reponsitories;
using PictureEngineer.Reponsitories.Firebase;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace PictureEngineer.Core
{
    public class Startup
    {
        public static IHostingEnvironment _env;
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsetting.json", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PictureEngineerDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PictureEngineer"), opt => opt.UseRowNumberForPaging());
            });
            //identity
            services.ConfigurateIdentityServer();
            //Cài đặt cache memory
            services.AddResponseCaching();
            services.AddMemoryCache();

            //cai dat cors cho phep goi api du domain khac
            services.AddCors();
            //cai dat serializer mvc
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                //filter validation
                options.Filters.Add<ValidationFilter>();
            })
                //fluent validator
                .AddFluentValidation(mvcConfiguration =>
                            mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>())

                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                //accept application/xml
                .AddXmlDataContractSerializerFormatters();

            //AutoMapper request domain
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //SPA with reactjs
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            //set cookies
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
            });
            //Authentication
            #region
            //setting cookies
            services.ConfigureApplicationCookie(options =>
            {
                //Cookies setting
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = false;
                //Dia chi tra ve khi chua dang nhap
                options.LoginPath = "/admin/login";
                //Dia chi tra ve khong cho phep truy cap
                options.AccessDeniedPath = "/Unauthorized";
                options.SlidingExpiration = true;
            });
            //Xác thực bẳng jwt và cookie authentication
            services.ConfigurationAuthentication();

            var applicationFirebase = Configuration.GetSection("Firebase");
            FirebaseConfig firebaseConfig = applicationFirebase.Get<FirebaseConfig>();

            //Set time out token confirm email
            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromMinutes(30));
            #endregion
            #region 
            //Cài đặt quyển đọc, thêm, sửa, xóa Policy
            services.AddPocilyRoles();
            #endregion
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAppContext>(x => new Core.AppContext(x.GetService<IHttpContextAccessor>()));

            services.AddScoped<IUnitOfWork>(x => new UnitOfWork(x.GetService<PictureEngineerDbContext>(), firebaseConfig.STORAGE_BUCKET));

            services.DependencyReponsitory();
           
            #region
            //Mailjet Service
            //Mail config
            var mailjetConfig = Configuration.GetSection("Mailjet");
            services.Configure<MailjetAttribute>(mailjetConfig);
            var mailjetApi = mailjetConfig.Get<MailjetAttribute>();

            services.AddHttpClient<IMailjetClient, MailjetClient>(client =>
            {
                client.SetDefaultSettings();
                
                client.UseBasicAuthentication(mailjetApi.ApiKey, mailjetApi.ApiSecret);
            });

            services.AddScoped<IEmailService>(x => 
                new EmailService(x.GetService<IMailjetClient>(), mailjetApi.EmailIdentityft, mailjetApi.NameIdentityft));
            #endregion
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(c => c.Run(async context =>
                {
                    var exception = context.Features
                        .Get<IExceptionHandlerPathFeature>()
                        .Error;
                    var result = JsonConvert.SerializeObject(new { error = exception.Message });
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }));
            }
            //Xác thực
            app.UseAuthentication();

            app.UseResponseCaching();
            app.UseCookiePolicy();
            //set up cors
            app.UseCors(builder => builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());
            //----------------------------
            app.UseHttpsRedirection();
            //Static file wwwroot
            app.UseStaticFiles();
            //SPA
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");

            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

        }
    }
}
