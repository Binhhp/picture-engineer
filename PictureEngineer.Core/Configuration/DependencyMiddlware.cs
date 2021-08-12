using Microsoft.Extensions.DependencyInjection;
using PictureEngineer.Data.Entities;
using PictureEngineer.Reponsitories;
using PictureEngineer.Reponsitories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Configuration
{
    public static class DependencyMiddlware
    {
        public static void DependencyReponsitory(this IServiceCollection services)
        {
            services.AddTransient<IReponsitoryArticle, ReponsitoryArticles>();
            services.AddTransient<IReponsitory<Services>, ReponsitoryServices>();
            services.AddTransient<IReponsitory<FAQs>, ReponsitoryFaqs>();
            services.AddTransient<IReponsitoryFile, ReponsitoryFiles>();
        }
    }
}
