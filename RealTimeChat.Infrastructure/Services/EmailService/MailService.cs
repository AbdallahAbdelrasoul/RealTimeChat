using RealTimeChat.Domain.Services.EmailService;
using RealTimeChat.Domain.Services.EmailService.IO;
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
                var from = new EmailAddress("abdallahabdelrasoul@gmail.com", "RealTimeChat Administration");
                var subject = input.Subject;
                var to = new EmailAddress(input.Email, "RealTimeChat User");
                var plainTextContent = "RealTimeChat Report";
                var htmlContent = input.Body;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
            catch
            {
                throw;
            }
        }
    }
}
