using RealTimeChat.Domain.Users;

namespace RealTimeChat.Domain.DomainServices
{
    public interface IUserDomainService
    {
        Task<User> Create(string email, string username, string password);
    }
}
