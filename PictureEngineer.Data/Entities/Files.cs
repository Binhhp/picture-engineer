using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PictureEngineer.Data.Entities
{
    [Table("Files")]
    public class Files
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
