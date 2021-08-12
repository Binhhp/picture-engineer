using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PictureEngineer.Data.Entities
{
    [Table("USERS")]
    public class AspNetUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Picture { get; set; }
    }
}
