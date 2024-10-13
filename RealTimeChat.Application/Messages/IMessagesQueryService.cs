using RealTimeChat.Application.Messages.DTOs;
using RealTimeChat.Domain.Shared.Pagination;

namespace RealTimeChat.Application.Messages
{
    public interface IMessagesQueryService
    {
        Task<PagedResponse<MessageDTO>> ListMessageHistory(MessageListHistoryDTO input);
    }
}
