using GustoHub.Data.Models;
using GustoHub.Data.ViewModels.GET;
using GustoHub.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace GustoHub.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendAdminApprovalRequestAsync(GETUserDto newUser)
        {
            // Get settings from configuration
            var emailSettings = configuration.GetSection("EmailSettings");
            var adminEmail = emailSettings["AdminEmail"];
            var senderEmail = emailSettings["SenderEmail"];
            var senderPassword = emailSettings["SenderPassword"];
            var smtpServer = emailSettings["SmtpServer"];
            var smtpPort = int.Parse(emailSettings["SmtpPort"]);

            var subject = "New User Access Request";
            var body = $@"
            A new user has requested access to the platform:
            Username: {newUser.Username}
            Registered At: {newUser.CreatedAt}

            Please review and approve their access in the admin panel.
        ";

            // Send email
            using var smtpClient = new SmtpClient(smtpServer, 465)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
                Timeout = 20000
            };

            smtpClient.UseDefaultCredentials = false;


            var mailMessage = new MailMessage(senderEmail, adminEmail, subject, body);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
