using RealTimeChat.Application.AuthenticationService.IO;
using RealTimeChat.Domain.DomainServices;
using RealTimeChat.Domain.Shared.Exceptions;
using RealTimeChat.Domain.Shared.Handlers;

namespace RealTimeChat.Application.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserDomainService _userDomainService;

        public AuthenticationService(IUserDomainService userDomainService)
        {
            _userDomainService = userDomainService;
        }

        public async Task<SignUpOutput> SignUp(SignUpInput input)
        {
            var user = await _userDomainService.Create(input.Email, input.UserName, input.Password);

            return SignUpOutput.FromUser(user);
        }
    }
}
