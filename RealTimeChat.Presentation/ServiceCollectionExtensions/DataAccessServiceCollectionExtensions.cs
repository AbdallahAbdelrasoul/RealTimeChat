using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess;

namespace RealTimeChat.Presentation.ServiceCollectionExtensions;
public static class DataAccessServiceCollectionExtensions
{
    private const string CONNECTION_STRING = "ChatDbConnectionString";
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString(CONNECTION_STRING);

        services.AddDbContext<ChatDbContext>(opt =>
            opt.UseNpgsql(connectionString)
            .EnableSensitiveDataLogging(true)
            .EnableDetailedErrors(true));

        return services;
    }
}