using RealTimeChat.Domain.Shared.Services.EmailService.IO;

namespace RealTimeChat.Domain.Shared.Services.EmailService
{
    public interface IMailService
    {
        Task SendEmailAsync(SendEmailInput input);
    }
}
