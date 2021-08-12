
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PictureEngineer.Data.Entities
{
        
    [Table("SERVICES")]
    public class Services
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Meta { get; set; }
                
        public string UserGuide{ get; set; }

        public string ImgPath { get; set; }

        public string Icon{ get; set; }

        public int ParentID{ get; set; }

        public string Color{ get; set; }

    }
}