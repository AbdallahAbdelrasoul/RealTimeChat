using RealTimeChat.Domain.Messages;
using RealTimeChat.Domain.Shared.Pagination;

namespace RealTimeChat.Domain.Repositories
{
    public interface IMessagesRepository
    {
        Task<int> Create(Message message);
        Task<PagedResponse<Message>> Search(int? userId, int? recipientId, int pageNumber, int pageSize);
    }
}
