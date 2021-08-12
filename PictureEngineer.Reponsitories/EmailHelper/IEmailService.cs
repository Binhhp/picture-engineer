using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureEngineer.Common.EmailHelper
{
    public interface IEmailService
    {
        Task<ResponseEmail> SendAsync(IdentityMessage identity);
    }
}
