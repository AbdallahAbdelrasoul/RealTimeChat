using RealTimeChat.Domain.Users;

namespace RealTimeChat.Application.Users.DTOs
{
    public class UserDTO
    {
        public int Id { get; private set; }
        public string UserName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public bool IsOnline { get; private set; }
        public string? ProcessId { get; private set; }
        internal static UserDTO FromUser(User user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsOnline = user.IsOnline,
                ProcessId = user.ProcessId,
            };
        }
    }
}
