using System.Net;
using System.Threading.Tasks;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Models.Email;
using GloboTicket.TicketManagement.Application.Models.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GloboTicket.TicketManagement.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        public EmailSettings EmailSettings { get; }

        public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger)
        {
            _logger = logger;
            EmailSettings = options.Value;
        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(EmailSettings.ApiKey);

            var subject = email.Subject;
            var emailBody = email.Body;
            var to = new EmailAddress(email.To);

            var from = new EmailAddress()
            {
                Email = EmailSettings.FromAddress,
                Name = EmailSettings.FromName
            };
            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            _logger.LogInformation("Email sent");
            if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
                return true;
            
            _logger.LogInformation("Email sending failed");

            return false;
        }
    }
}