using RealTimeChat.Domain.Users;

namespace RealTimeChat.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<int> Create(User user);
    }
}
