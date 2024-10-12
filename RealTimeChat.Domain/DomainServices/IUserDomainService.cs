using RealTimeChat.Domain.Users;

namespace RealTimeChat.Domain.DomainServices
{
    public interface IUserDomainService
    {
        Task<User> CreateNewUser(string email, string username, string password);
    }
}
