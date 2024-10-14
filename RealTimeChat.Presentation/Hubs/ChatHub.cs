using Microsoft.AspNetCore.SignalR;
using RealTimeChat.Domain.Messages;
using RealTimeChat.Domain.Repositories;
using RealTimeChat.Domain.Shared;
using RealTimeChat.Domain.Shared.Validation;

namespace RealTimeChat.Presentation.Hubs
{

    public class ChatHub : Hub
    {
        private readonly ActiveContext _activeContext;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IValidationEngine _validationEngine;

        public ChatHub(ActiveContext activeContext, IMessagesRepository messagesRepository, IValidationEngine validationEngine)
        {
            _activeContext = activeContext;
            _messagesRepository = messagesRepository;
            _validationEngine = validationEngine;
        }

        public async Task SendMessage(int? recipientId, string content)
        {
            var username = Context.User?.Identity?.Name;
            var userId = _activeContext.Id;

            Message message = Message.Create(null, userId, recipientId, content);

            await message.Create(_messagesRepository, _validationEngine);

            await Clients.All.SendAsync("ReceiveMessage", userId, message.RecipientId, message.Content);
        }
    }

}
