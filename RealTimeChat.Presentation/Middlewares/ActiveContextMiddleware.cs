using RealTimeChat.Domain.Shared;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace RealTimeChat.Presentation.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class ActiveContextMiddleware
    {
        private readonly RequestDelegate _next;

        public ActiveContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ActiveContext activeContext)
        {
            context?.User?.Claims?.Append(new Claim(nameof(ActiveContext.RequestId), activeContext.RequestId));
            context?.Request?.Headers?.Add(nameof(ActiveContext.RequestId), activeContext.RequestId);
            if (!context?.User?.Identity?.IsAuthenticated ?? false)
            {
                await _next(context!);
                return;
            }

            FillActiveContext(activeContext, context!);
            await _next(context!);
        }

        private static void FillActiveContext(ActiveContext activeContext, HttpContext context)
        {
            activeContext.Id = int.Parse(context?.User?.Claims?.FirstOrDefault(w => w.Type == nameof(ActiveContext.Id))?.Value ?? "");
            if (!string.IsNullOrEmpty(context?.User?.Claims?.FirstOrDefault(w => w.Type == nameof(ActiveContext.TenenatId))?.Value))
            {
                activeContext.TenenatId = int.Parse(context?.User?.Claims?.FirstOrDefault(w => w.Type == nameof(ActiveContext.TenenatId))?.Value ?? "");
            }
            activeContext.Roles = GetRoles(context!);
            activeContext.SessionId = ExtractClaimValue(context!, nameof(ActiveContext.SessionId));
            activeContext.DisplayName = ExtractClaimValue(context!, nameof(ActiveContext.DisplayName));
            activeContext.Cookie = ExtractClaimValue(context!, nameof(ActiveContext.Cookie));
            activeContext.EmailAddress = ExtractClaimValue(context!, nameof(ActiveContext.EmailAddress));
        }
        private static string[] GetRoles(HttpContext context) =>
            context?.User?.Claims?.Where(w => w.Type == ClaimTypes.Role)?.Select(s => s.Value)?.ToArray() ?? Array.Empty<string>();

        private static string ExtractClaimValue(HttpContext context, string type) =>
            context?.User?.Claims?.FirstOrDefault(w => w.Type == type)?.Value ?? "";
    }
}