using RealTimeChat.Domain.Shared.Services.EmailService;
using RealTimeChat.Domain.Shared.Services.EmailService.IO;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RealTimeChat.Infrastructure.Services.EmailService
{
    public class MailService : IMailService
    {
        public async Task SendEmailAsync(SendEmailInput input)
        {
            try
            {
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("einvoice@applogica.com", "E-Commerce Administration");
                var subject = input.Subject;
                var to = new EmailAddress(input.Email, "E-Commerce User");
                var plainTextContent = "E-Commerce Report";
                var htmlContent = input.Body;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception)
            {
            }
        }
    }
}
