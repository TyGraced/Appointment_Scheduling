using Appointment_Scheduling.Application.Services.Interfaces;
using Appointment_Scheduling.Core.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Appointment_Scheduling.Application.Services.Implementations
{
    public class GmailService : IEmailService
    {
        private readonly GmailSettings _settings;

        public GmailService(IOptions<GmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using var smtpClient = new SmtpClient(_settings.Host, _settings.Port)
                {
                    Credentials = new NetworkCredential(_settings.SenderEmail, _settings.Password),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.SenderEmail!, _settings.SenderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email:{ex.Message}");
                return false;
            }
        }
    }
}
