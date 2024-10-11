using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealTimeChat.Domain.Shared.Handlers
{
    public class AuthenticationHandler
    {
        private readonly IConfiguration _configuration;
        private readonly string _tokenSecret;
        private const string TokenSecretKey = "Auth:Secret";
        private readonly int _tokenExpirationSeconds;
        private const string TokenExpirationSecondesKey = "Auth:Expiration";

        public AuthenticationHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenSecret = _configuration[TokenSecretKey] ?? "GeekLabsHolding - Awesome - Secret";
            _tokenExpirationSeconds = int.Parse(_configuration[TokenExpirationSecondesKey] ?? "3600");
        }

        public string GenerateJwtToken(ActiveContext context)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSecret));
            var token = new JwtSecurityToken(
                    audience: AuthenticationConstants.Audience,
                    expires: DateTime.Now.AddSeconds(_tokenExpirationSeconds),
                    claims: GetClaims(context),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static List<Claim> GetClaims(ActiveContext context)
        {
            var list = new List<Claim>();

            AddClaim(nameof(ActiveContext.Cookie), context.Cookie, list);
            AddClaim(nameof(ActiveContext.DisplayName), context.DisplayName, list);
            AddClaim(nameof(ActiveContext.Id), context.Id, list);
            AddClaim(nameof(ActiveContext.SessionId), context.SessionId, list);
            AddClaim(nameof(ActiveContext.EmailAddress), context.EmailAddress, list);
            AddClaim(nameof(ActiveContext.TenenatId), context.TenenatId, list);

            if (context.Roles is not null)
            {
                foreach (var item in context.Roles)
                {
                    AddClaim(ClaimTypes.Role, item, list);
                }
            }

            return list;
        }

        private static void AddClaim(string name, object? value, List<Claim> source)
        {
            if (value is null)
            {
                return;
            }
            source.Add(new Claim(name, value?.ToString() ?? ""));
        }
    }
}
