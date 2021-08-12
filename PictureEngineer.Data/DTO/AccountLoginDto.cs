using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.Data.DTO
{
   public class AccountLoginDto
    {
        public string ReturnUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
