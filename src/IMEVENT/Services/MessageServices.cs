using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IMEVENT.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        string baseSendUrl = "https://api.sendgrid.com/v3/mail/send";
        public AuthMessageSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; }  // set only via Secret Manager
        private string getBody(string email, string subject, string message )
        {
            String toreturn = "{ \"personalizations\": [   {    \"to\": [      {         \"email\": \"TOEMAIL\"        }     ],      \"subject\": \"SUBJECT\"    }  ],  \"from\": {    \"email\": \"ppd@ppd.dev\"  }, \"content\": [   {      \"type\": \"text\",      \"value\": \"CONTENT\"    }  ]}";
            toreturn=toreturn.Replace("TOEMAIL", email);
            toreturn= toreturn.Replace("SUBJECT", subject);
            toreturn = toreturn.Replace("CONTENT", message);
            return toreturn;
        }
        public  Task SendEmailAsync(string email, string subject, string message)
        {
            string emailUser = Options.SendGridUser;
            string emailKey = Options.SendGridKey;
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseSendUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + emailKey);
                //client.DefaultRequestHeaders.Add("Content-Length", "application/json ");
                string rawBody = getBody(email, subject, message);
                StringContent data = new StringContent(rawBody, Encoding.UTF8,
                                    "application/json");
    
                Task< HttpResponseMessage> response =  client.PostAsync(client.BaseAddress, data);
                HttpResponseMessage res = response.Result;
                return response;
                
    
            }
            
            // Create a Web transport for sending email.
  
        
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
