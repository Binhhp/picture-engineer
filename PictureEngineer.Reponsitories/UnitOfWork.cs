using PictureEngineer.Data;
using PictureEngineer.Data.Entities;
using PictureEngineer.Reponsitories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.Reponsitories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IReponsitoryArticle Articles { get; private set; }
        public IReponsitory<Services> Services { get; private set; }
        public IReponsitory<FAQs> FAQs { get; private set; }
        public IReponsitoryFile Files { get; private set; }

        public UnitOfWork(PictureEngineerDbContext context, string storageBuket)
        {
            Articles = new ReponsitoryArticles(context, storageBuket);
            Services = new ReponsitoryServices(context);
            FAQs = new ReponsitoryFaqs(context);
            Files = new ReponsitoryFiles(context, storageBuket);
        }
        
    }
}
