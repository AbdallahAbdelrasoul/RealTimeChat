using RealTimeChat.Domain.Services.EmailService.IO;

namespace RealTimeChat.Domain.Services.EmailService
{
    public interface IMailService
    {
        Task SendEmailAsync(SendEmailInput input);
    }
}
