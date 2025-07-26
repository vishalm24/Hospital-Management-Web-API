using Hospital_Management.Services.IServices;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.Net;
using System.Net.Mail;

namespace Hospital_Management.Services
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        private string _email;
        private string _password;
        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _email = Environment.GetEnvironmentVariable("User_Email");
            _password = Environment.GetEnvironmentVariable("User_Password");
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body, bool IsHtml = false)
        {
            try
            {
                var smtpClient = new SmtpClient(_configuration["SmtpSettings:Host"])
                {
                    Port = int.Parse(_configuration["SmtpSettings:Port"]),
                    Credentials = new NetworkCredential(_email, _password),
                    EnableSsl = true,
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_email),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {To}", toEmail);
                throw;
            }
        }
    }
}
