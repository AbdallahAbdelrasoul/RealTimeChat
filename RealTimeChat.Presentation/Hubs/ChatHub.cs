using Microsoft.AspNetCore.SignalR;
using RealTimeChat.Application.Messages;
using RealTimeChat.Domain.Shared;

namespace RealTimeChat.Presentation.Hubs
{

    public class ChatHub : Hub
    {
        private readonly ActiveContext _activeContext;
        private readonly IMessagesService _messagesService;

        public ChatHub(IMessagesService messagesService, ActiveContext activeContext)
        {
            _messagesService = messagesService;
            _activeContext = activeContext;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", "content");
        }
        public async Task SendMessage(int? recipientId, string content)
        {
            //var username = Context.User?.Identity?.Name;
            var userId = _activeContext.Id;

            await Clients.All.SendAsync("ReceiveMessage", userId, recipientId, content);

            await _messagesService.Create(new()
            {
                Content = content,
                RecipientId = recipientId
            });
        }
    }

}
