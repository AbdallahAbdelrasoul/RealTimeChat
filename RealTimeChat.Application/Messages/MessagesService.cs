using RealTimeChat.Application.Messages.DTOs;
using RealTimeChat.Domain.Messages;
using RealTimeChat.Domain.Repositories;
using RealTimeChat.Domain.Shared;
using RealTimeChat.Domain.Shared.Validation;

namespace RealTimeChat.Application.Messages
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessagesRepository _messagesRepository;
        private readonly IValidationEngine _validationEngine;
        private readonly ActiveContext _activeContext;
        public MessagesService(IMessagesRepository messagesRepository, IValidationEngine validationEngine, ActiveContext activeContext)
        {
            _messagesRepository = messagesRepository;
            _validationEngine = validationEngine;
            _activeContext = activeContext;
        }

        public async Task<MessageDTO> Create(MessageCreateDTO input)
        {
            var userId = _activeContext.Id;
            var msg = input.ToMessage(senderId: userId);

            await msg.Create(_messagesRepository, _validationEngine);

            return MessageDTO.FromMessage(msg);
        }

        public async Task MarkMessagesAsSeen(List<int> messagesIds)
        {
            await Message.MarkAsSeen(_messagesRepository, messagesIds);
        }
    }
}
