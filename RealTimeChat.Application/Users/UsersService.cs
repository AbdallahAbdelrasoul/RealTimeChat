using RealTimeChat.Application.Users.DTOs;
using RealTimeChat.Domain.Repositories;
using RealTimeChat.Domain.Services.EmailService;
using RealTimeChat.Domain.Services.EmailService.IO;
using RealTimeChat.Domain.Shared;
using RealTimeChat.Domain.Shared.Exceptions;
using RealTimeChat.Domain.Shared.Handlers;
using RealTimeChat.Domain.Shared.Validation;
using RealTimeChat.Domain.Users;

namespace RealTimeChat.Application.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IValidationEngine _validationEngine;
        private readonly AuthenticationHandler _authenticationHandler;
        private readonly IMailService _mailService;
        public UsersService(IUsersRepository userRepository, IValidationEngine validationEngine, AuthenticationHandler authenticationHandler, IMailService mailService)
        {
            _userRepository = userRepository;
            _validationEngine = validationEngine;
            _authenticationHandler = authenticationHandler;
            _mailService = mailService;
        }

        public async Task<UserDTO> Register(UserRegisterDTO input)
        {
            if (!PasswordHandler.IsPasswordComplex(input.Password))
            {
                throw new DataNotValidException("Password is not complex");
            }

            var (hash, salt) = PasswordHandler.HashPassword(input.Password);
            var user = User.Create(null, input.Email, input.UserName, hash, salt);

            user.SetProcessId(Guid.NewGuid().ToString());
            user.SetEmailOTP(AuthenticationHandler.GenerateOTP());

            await user.Create(_userRepository, _validationEngine);

            await _mailService.SendEmailAsync(new SendEmailInput
            {
                Body = user.EmailOTP!,
                Email = input.Email,
                Subject = "RealTimeChat Email OTP"
            });

            return UserDTO.FromUser(user);
        }

        public async Task<UserLoginOutputDTO> Login(UserLoginDTO input)
        {
            if (!PasswordHandler.IsPasswordComplex(input.Password))
            {
                throw new DataNotValidException("Login Invalid");
            }

            var user = await User.GetByUsername(input.UserName, _userRepository);

            if (!user.IsVerified)
            {
                throw new UnauthorizedException("Your email is not verified");
            }

            if (!PasswordHandler.VerifyPassword(user.PasswordHash, user.PasswordSalt, input.Password))
            {
                throw new UnauthorizedException("Login Invalid");
            }

            var context = new ActiveContext
            {
                Id = user.Id,
                DisplayName = user.UserName,
                EmailAddress = user.Email,
                SessionId = Guid.NewGuid().ToString(),
                TenenatId = 1
            };

            user.SetIsOnline(true);
            await user.Update(_userRepository, _validationEngine);

            var token = _authenticationHandler.GenerateJwtToken(context);
            return new UserLoginOutputDTO
            {
                Token = token
            };
        }

        public async Task VerifyEmail(UserValidateEmailDTO input)
        {
            var user = await User.GetByProcessId(input.ProcessId, _userRepository);
            if (user.EmailOTP == input.OTP)
            {
                user.SetIsVerified(true);
            }
            else
            {
                throw new DataNotValidException("Invalid OTP");
            }

            await user.Update(_userRepository, _validationEngine);
        }
    }
}
