using RealTimeChat.Domain.DomainServices;
using RealTimeChat.Domain.Shared;
using RealTimeChat.Domain.Shared.Handlers;
using RealTimeChat.Domain.Shared.Validation;

namespace RealTimeChat.Presentation.ServiceCollectionExtensions
{
    public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ActiveContext>();
            services.AddScoped<AuthenticationHandler>();

            services.AddScoped<IUserDomainService, UserDomainService>();
            services.AddScoped<IValidationEngine, ValidationEngine>();

            return services;
        }
    }
}