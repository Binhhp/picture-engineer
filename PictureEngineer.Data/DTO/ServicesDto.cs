using PictureEngineer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.Data.DTO
{
    public class ServicesDto
    {
        public int Id{ get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Meta { get; set; }

        public string UserGuide { get; set; }

        public string ImgPath { get; set; }

        public string Icon { get; set; }

        public int ParentID { get; set; }

        public string Color { get; set; }

        public List<FAQs> FAQs { get; set; }
    }
}
