using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace PictureEngineer.Common.EmailHelper
{
    public class EmailService : IEmailService
    {
        private readonly IMailjetClient _mailjetClient;
        private readonly string NameIdentityft;
        private readonly string EmailIdentityft;

        public EmailService(IMailjetClient mailjetClient, 
            string emailIdentityft, string nameIdentityft)
        {
            _mailjetClient = mailjetClient;
            EmailIdentityft = emailIdentityft;
            NameIdentityft = nameIdentityft;
        }
        /// <summary>
        /// Send mail using mailjet api
        /// </summary>
        /// <param name="message">As subject, body, Destination, email to, name to</param>
        /// <returns>Send mail using mailjet api</returns>
        public async Task<ResponseEmail> SendAsync(IdentityMessage message)
        {
            try
            {
                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource,
                }.Property(Send.FromEmail, EmailIdentityft)
                    .Property(Send.FromName, NameIdentityft)
                    .Property(Send.Subject, message.Subject)
                    .Property(Send.TextPart, message.Destination)
                    .Property(Send.HtmlPart, message.Body)
                    .Property(Send.Recipients, new JArray
                    {
                        new JObject
                        {
                            { "Email", message.EmailAddress },
                            { "Name", message.NameObject }
                        }
                    });

                MailjetResponse response = await _mailjetClient.PostAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return new ResponseEmail { Successed = true, Code = 201, ErrorMessage = "" };
                }
                else
                {
                    return new ResponseEmail {
                        Successed = false,
                        Code = response.StatusCode,
                        ErrorMessage = response.GetErrorMessage(),
                        ErrorInfo = response.GetErrorInfo()
                    };
                }
            }
            catch(Exception ex)
            {
                return new ResponseEmail { Successed = false, Code = 400, ErrorMessage = ex.Message };
            }
        }
    }
}
