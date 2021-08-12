using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.Data.DTO
{
    public class ProfilePassword
    {
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
        public string ConfirmPasswordNew { get; set; }
    }
}
