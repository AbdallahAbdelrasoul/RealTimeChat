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
        private readonly ChatDbContext _dbcontext;

        public UserRepository(ChatDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<int> Create(User user)
        {
            try
            {
                await _dbcontext.Users.AddAsync(user);
                await _dbcontext.SaveChangesAsync();
                return user.Id;
            }
            catch (DbUpdateException e)
                when (e.InnerException is NpgsqlException { SqlState: PostgresErrorCodes.UniqueViolation })
            {
                throw new DataDuplicateException("User is already exists.");
            }
        }
    }
}
