using System.ComponentModel.DataAnnotations;

namespace RealTimeChat.Application.Users.DTOs
{
    public class UserValidateEmailDTO
    {
        public string ProcessId { get; set; } = string.Empty;

        [MaxLength(6)]
        public string OTP { get; set; } = string.Empty;
    }
}
