using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PictureEngineer.Data.Entities
{
    [Table("BLOG")]
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string ImagePath { get; set; }
        
        [Required]
        public string ImageName { get; set; }
        
        [Required]
        public string Contents { get; set; }
        
        [Required]
        [StringLength(100)]
        public string MetaTitle { get; set; }

        public DateTime DateCreated { get; set; }

		public int ServiceId { get; set; }
	}
}
