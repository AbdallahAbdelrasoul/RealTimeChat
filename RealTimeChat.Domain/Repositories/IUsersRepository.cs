using RealTimeChat.Domain.Users;

namespace RealTimeChat.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<int> Create(User user);
        Task<User?> GetByUsername(string username);
        Task<User?> GetByProcessId(string processId);
        Task<bool> Update(User user);
    }
}
