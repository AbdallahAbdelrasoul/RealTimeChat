using RealTimeChat.Domain.Shared.Validation;

namespace RealTimeChat.Presentation.ServiceCollectionExtensions
{
    public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {

            services.AddScoped<IValidationEngine, ValidationEngine>();

            return services;
        }
    }
}