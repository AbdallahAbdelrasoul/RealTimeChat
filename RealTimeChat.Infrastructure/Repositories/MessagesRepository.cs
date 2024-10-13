using Microsoft.EntityFrameworkCore;
using RealTimeChat.DataAccess;
using RealTimeChat.Domain.Messages;
using RealTimeChat.Domain.Repositories;
using RealTimeChat.Domain.Shared.Pagination;

namespace RealTimeChat.Infrastructure.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly ChatDbContext _dbContext;

        public MessagesRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(Message message)
        {
            await _dbContext.AddAsync(message);
            await _dbContext.SaveChangesAsync();
            return message.Id;
        }

        public async Task<PagedResponse<Message>> Search(int? userId, int? recipientId, int pageNumber, int pageSize)
        {
            var query = _dbContext.Messages.AsQueryable();

            if (userId == null)
            {
                query = query.Where(x => x.SenderId == userId);
            }

            if (recipientId == null)
            {
                query = query.Where(x => x.RecipientId == recipientId);
            }

            return new PagedResponse<Message>
            {
                TotalCount = await query.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }
    }
}
