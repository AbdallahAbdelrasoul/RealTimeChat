﻿namespace RealTimeChat.Domain.Services.EmailService.IO
{
    public class SendEmailInput
    {
        public string Email { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
    }
}
