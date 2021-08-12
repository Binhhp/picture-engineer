using PictureEngineer.Data.Entities;
using PictureEngineer.Reponsitories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.Reponsitories
{ 
    public interface IUnitOfWork
    {
        IReponsitoryArticle Articles { get; }
        IReponsitory<Services> Services { get; }
        IReponsitory<FAQs> FAQs { get; }
        IReponsitoryFile Files { get; }
    }
}
