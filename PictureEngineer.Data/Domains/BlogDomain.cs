using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.Data.Domains
{
    public class BlogDomain
    {
        public int ServiceId { get; set; }
        public string ImagePath { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Contents { get; set; }

        public string MetaTitle { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
