using RealTimeChat.Application.Messages.DTOs;
using RealTimeChat.Domain.Messages;
using RealTimeChat.Domain.Repositories;
using RealTimeChat.Domain.Shared;
using RealTimeChat.Domain.Shared.Pagination;

namespace RealTimeChat.Application.Messages
{
    public class MessagesQueryService : IMessagesQueryService
    {
        private readonly ActiveContext _activeContext;
        private readonly IMessagesRepository _messagesRepository;
        public MessagesQueryService(ActiveContext activeContext, IMessagesRepository messagesRepository)
        {
            _activeContext = activeContext;
            _messagesRepository = messagesRepository;
        }

        public async Task<PagedResponse<MessageDTO>> ListMessageHistory(MessageListHistoryDTO input)
        {
            var userId = _activeContext.Id;
            var messages = await Message.Search(_messagesRepository, userId, input.RecipientId, input.PageNo, input.PageSize);

            return new PagedResponse<MessageDTO>
            {
                TotalCount = messages.TotalCount,
                PageNumber = messages.PageNumber,
                PageSize = messages.PageSize,
                Data = messages.Data.Select(x => MessageDTO.FromMessage(x)).ToList(),
            };
        }
    }
}
