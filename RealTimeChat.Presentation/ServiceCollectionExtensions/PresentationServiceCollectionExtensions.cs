using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RealTimeChat.Domain.Shared;
using System.Text;

namespace RealTimeChat.Presentation.ServiceCollectionExtensions
{
    public static class PresentationServiceCollectionExtensions
    {
        private const string TokenSecretKey = "Auth:Secret";
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            string tokenSecret = configuration[TokenSecretKey] ?? AuthenticationConstants.TokenSecret;
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

                // Make SignalR work with JWT in WebSockets
                //options.Events = new JwtBearerEvents
                //{
                //    OnMessageReceived = context =>
                //    {
                //        var accessToken = context.Request.Query["access_token"];

                //        // If the request is for the SignalR hub
                //        var path = context.HttpContext.Request.Path;
                //        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments($"/{nameof(ChatHub)}"))
                //        {
                //            // Read the token from the query string
                //            context.Token = accessToken;
                //        }
                //        return Task.CompletedTask;
                //    }
                //};
            });

            return services;
        }
    }
}
