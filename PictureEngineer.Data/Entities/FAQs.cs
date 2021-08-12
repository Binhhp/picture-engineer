using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PictureEngineer.Data.Entities
{
    [Table("FAQS")]
    public class FAQs
    {
        [Key]
        public int Id{ get; set; }

        public string Question{ get; set; }

        public string Answer{ get; set; }

        public int ServiceID{ get; set; }
    }
}