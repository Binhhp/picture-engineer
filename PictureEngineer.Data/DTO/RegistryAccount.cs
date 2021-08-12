using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.Data.DTO
{
    public class RegistryAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
    }
}
