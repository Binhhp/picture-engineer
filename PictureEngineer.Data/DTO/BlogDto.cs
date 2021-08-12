using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.Data.DTO
{
    public class BlogDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Contents { get; set; }

        public string MetaTitle { get; set; }

        public int ServiceId { get; set; }

        public IFormFile FileUpload { get; set; }
    }
}
