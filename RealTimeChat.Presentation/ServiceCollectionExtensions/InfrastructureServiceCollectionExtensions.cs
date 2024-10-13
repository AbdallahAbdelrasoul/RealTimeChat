using RealTimeChat.Domain.Repositories;
using RealTimeChat.Domain.Services.EmailService;
using RealTimeChat.Infrastructure.Repositories;
using RealTimeChat.Infrastructure.Services.EmailService;

namespace RealTimeChat.Presentation.ServiceCollectionExtensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IMailService, MailService>();

        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IMessagesRepository, MessagesRepository>();

        return services;
    }
}