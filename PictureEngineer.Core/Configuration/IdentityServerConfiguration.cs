using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PictureEngineer.Data;
using PictureEngineer.Data.Entities;
using System;

namespace PictureEngineer.Core.Configuration
{
    public static class IdentityServerConfiguration
    {
        public static void ConfigurateIdentityServer(this IServiceCollection services)
        {
            services.AddIdentity<AspNetUser, IdentityRole>(options => {
                //Thiet lap password
                options.Password = new PasswordOptions
                {
                    RequiredLength = 4,//So ky tu toi thieu
                    RequireDigit = false,//Khong bat buoc phai co so
                    RequireLowercase = false,//Khong bat buoc phai co chu thuong
                    RequireUppercase = false,//Khong bat buoc chu in
                    RequireNonAlphanumeric = false//Khong bat buoc co ky tu dac biet
                };
                //cau hinh lockout - khoa user
                options.Lockout = new LockoutOptions
                {
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),// Khoa 5 phut
                    MaxFailedAccessAttempts = 5,//That bai 5 lan thi khoa
                    AllowedForNewUsers = true
                };
                //cau hinh user
                options.User = new UserOptions
                {
                    AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+",//Cac ky tu dat ten user
                    RequireUniqueEmail = true, //Email la duy nhat
                };
                //cau hinh dang nhap
                options.SignIn = new SignInOptions
                {
                    RequireConfirmedEmail = true,//cau hinh xac thuc dia chi email (email phai ton tai)
                    RequireConfirmedPhoneNumber = false//cau hinh xac thuc so dien thoai
                };

            })
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddUserManager<UserManager<AspNetUser>>()
                .AddEntityFrameworkStores<PictureEngineerDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
