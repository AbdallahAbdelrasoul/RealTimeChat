using Microsoft.EntityFrameworkCore;
using Npgsql;
using RealTimeChat.DataAccess;
using RealTimeChat.Domain.Repositories;
using RealTimeChat.Domain.Shared.Exceptions;
using RealTimeChat.Domain.Users;

namespace RealTimeChat.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _dbContext;

        public UserRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(User user)
        {
            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return user.Id;
            }
            catch (DbUpdateException e)
                when (e.InnerException is NpgsqlException { SqlState: PostgresErrorCodes.UniqueViolation })
            {
                throw new DataDuplicateException("User is already exists.");
            }
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<User?> GetByProcessId(string processId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.ProcessId == processId);
        }

        public async Task<bool> Update(User user)
        {
            if (_dbContext.ChangeTracker.Entries<User>().Any(a => a.State == EntityState.Modified))
            {
                return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
