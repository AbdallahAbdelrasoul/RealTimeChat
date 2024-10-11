using RealTimeChat.Domain.Users;

namespace RealTimeChat.Application.AuthenticationService.IO
{
    public class SignUpOutput
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsOnline { get; private set; }

        internal static SignUpOutput FromUser(User user)
        {
            return new SignUpOutput()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsOnline = user.IsOnline,
            };
        }
    }
}
