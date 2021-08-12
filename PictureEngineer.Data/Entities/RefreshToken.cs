using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PictureEngineer.Data.Entities
{
    [Table("RefreshToken")]
    public class RefreshToken{

        [Key]
        public int Id { get; set; }

        public string JwtId { get; set; }

        public DateTime Expires{ get;set;}

        public bool IsUsed { get; set; }

        public bool IsExpired { get; set; }

        public DateTime DateCreated { get;set; }

        public DateTime? Revoked { get;set; }

        public string TokenRefresh { get; set; }

        public string ReplaceByToken{ get; set; }

        public bool IsRevoked { get; set; }
        
    }
}