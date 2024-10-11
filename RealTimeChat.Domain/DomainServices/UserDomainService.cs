using RealTimeChat.Domain.Repositories;
using RealTimeChat.Domain.Shared.Exceptions;
using RealTimeChat.Domain.Shared.Handlers;
using RealTimeChat.Domain.Shared.Validation;
using RealTimeChat.Domain.Users;

namespace RealTimeChat.Domain.DomainServices
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidationEngine _validationEngine;
        public UserDomainService(IUserRepository userRepository, IValidationEngine validationEngine)
        {
            _userRepository = userRepository;
            _validationEngine = validationEngine;
        }

        public async Task<User> Create(string email, string username, string password)
        {
            if (!PasswordHandler.IsPasswordComplex(password))
            {
                throw new DataNotValidException("Password is not complex");
            }

            var (hash, salt) = PasswordHandler.HashPassword(password);
            var user = User.Create(null, email, username, hash, salt);
            await user.Create(_userRepository, _validationEngine);
            return user;
        }
    }

}
