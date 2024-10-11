using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RealTimeChat.Domain.Shared;
using System.Text;

namespace RealTimeChat.Presentation.ServiceCollectionExtensions
{
    public static class PresentationServiceCollectionExtensions
    {
        private const string TokenSecretKey = "Auth:Secret";
        public static IServiceCollection AddPresentationLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            string tokenSecret = configuration[TokenSecretKey] ?? "RealTimeChat - Awesome - Secret";
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidAudience = AuthenticationConstants.Audience,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret))
                };
            });

            return services;
        }
    }
}
