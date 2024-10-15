using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RealTimeChat.Application.Messages;
using RealTimeChat.Domain.Shared;
using System.Security.Claims;

namespace RealTimeChat.Presentation.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly IMessagesService _messagesService;
        private readonly ActiveContext _activeContext;
        public ChatHub(IMessagesService messagesService, ActiveContext activeContext)
        {
            _messagesService = messagesService;
            _activeContext = activeContext;
        }

        public override async Task OnConnectedAsync()
        {
            var c = Context;
            FillActiveContext(_activeContext, Context.GetHttpContext()!);

            await Clients.All.SendAsync("UserConnected", _activeContext.Id);
        }
        public async Task SendMessage(int? recipientId, string content)
        {
            FillActiveContext(_activeContext, Context.GetHttpContext()!);
            var userId = _activeContext.Id;

            await Clients.All.SendAsync("ReceiveMessage", userId, recipientId, content);

            await _messagesService.Create(new()
            {
                Content = content,
                RecipientId = recipientId
            });
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            FillActiveContext(_activeContext, Context.GetHttpContext()!);
            var userId = _activeContext.Id;

            await Clients.All.SendAsync("UserDisconnected", userId);

            await base.OnDisconnectedAsync(exception);
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
