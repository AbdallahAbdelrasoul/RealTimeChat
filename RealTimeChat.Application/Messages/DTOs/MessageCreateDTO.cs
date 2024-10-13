using RealTimeChat.Domain.Messages;

namespace RealTimeChat.Application.Messages.DTOs
{
    public class MessageCreateDTO
    {
        public int? RecipientId { get; set; }
        public string Content { get; set; } = string.Empty;

        public Message ToMessage(int senderId) => Message.Create(null, senderId, RecipientId, Content);
    }
}
