using RealTimeChat.Application.Users.DTOs;

namespace RealTimeChat.Application.Users
{
    public interface IUsersService
    {
        Task<UserDTO> Register(UserRegisterDTO input);
        Task<UserLoginOutputDTO> Login(UserLoginDTO input);
        Task VerifyEmail(UserValidateEmailDTO input);
    }
}
