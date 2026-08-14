using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Restaurant.Application.Services.Auth;
using Restaurant.Contract.Settings.Auth;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Services.Auth
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailVerificationAsync(string toEmail, string token)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.SenderEmail);
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = "Verify your email address";

            var builder = new BodyBuilder
            {
                // NOTE: The verification URL should point to your frontend or API endpoint.
                // Assuming an API endpoint for simplicity.
                HtmlBody = $"<p>Please verify your email using this token: {token}</p>"
            };
            
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.AppPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
