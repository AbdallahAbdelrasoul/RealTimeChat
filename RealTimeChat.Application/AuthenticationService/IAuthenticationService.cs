using RealTimeChat.Application.AuthenticationService.IO;
using RealTimeChat.Domain.Users;

namespace RealTimeChat.Application.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<SignUpOutput> SignUp(SignUpInput input);
    }
}
