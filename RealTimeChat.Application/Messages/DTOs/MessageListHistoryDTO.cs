using RealTimeChat.Domain.Shared.Pagination;

namespace RealTimeChat.Application.Messages.DTOs
{
    public class MessageListHistoryDTO : PagedRequest
    {
        public int? RecipientId { get; set; }
    }
}
