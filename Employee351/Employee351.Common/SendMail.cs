using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee351.Common
{
    public class SendMail
    {
        private IConfiguration _configuration;
        public SendMail(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  async Task SendEmail(string email, string subject, string body)
        {
            var emailSettings = _configuration.GetSection("MailSettings");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Shaligram Infotech", emailSettings["UserName"]));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(emailSettings["UserName"], emailSettings["Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
