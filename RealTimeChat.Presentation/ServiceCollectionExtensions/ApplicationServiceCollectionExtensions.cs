using RealTimeChat.Application.Users;

namespace RealTimeChat.Presentation.ServiceCollectionExtensions;

public static class ApplicationServiceCollectionExtensions
{

    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();

        return services;
    }
}