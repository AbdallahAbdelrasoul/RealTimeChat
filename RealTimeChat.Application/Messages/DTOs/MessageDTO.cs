using RealTimeChat.Domain.Messages;

namespace RealTimeChat.Application.Messages.DTOs
{
    public class MessageDTO
    {
        public int Id { get; private set; }
        public int SenderId { get; private set; }
        public int? RecipientId { get; private set; }
        public string Content { get; private set; } = string.Empty;
        public DateTime Timestamp { get; private set; }
        public bool IsSeen { get; private set; } = false;

        public static MessageDTO FromMessage(Message message)
            => new()
            {
                Id = message.Id,
                SenderId = message.SenderId,
                RecipientId = message.RecipientId,
                Content = message.Content,
                Timestamp = message.Timestamp.ToLocalTime(),
                IsSeen = message.IsSeen,
            };
    }
}
