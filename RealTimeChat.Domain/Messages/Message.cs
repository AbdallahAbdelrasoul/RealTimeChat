using FluentValidation;
using RealTimeChat.Domain.Repositories;
using RealTimeChat.Domain.Shared.Aggregates;
using RealTimeChat.Domain.Shared.Pagination;
using RealTimeChat.Domain.Shared.Validation;

namespace RealTimeChat.Domain.Messages
{
    public class Message : BaseDomain, IValidationModel<Message>
    {
        private Message()
        {
            Content = string.Empty;
        }
        public int Id { get; private set; }
        public int SenderId { get; private set; }
        public int? RecipientId { get; private set; }
        public string Content { get; private set; }
        public DateTime Timestamp { get; private set; }
        public bool IsSeen { get; private set; } = false;
        public AbstractValidator<Message> Validator => throw new NotImplementedException();

        public static Message Create(int? id, int senderId, int? recipientId, string content)
            => new()
            {
                Id = id ?? 0,
                SenderId = senderId,
                RecipientId = recipientId,
                Content = content,
                Timestamp = DateTime.Now.ToUniversalTime(),
                IsSeen = false
            };

        public async Task<int> Create(IMessagesRepository repository, IValidationEngine validation)
        {
            //validation.Validate(this);
            return await repository.Create(this);
        }

        public static async Task<PagedResponse<Message>> Search(IMessagesRepository repository, int? userId, int? recipientId, int pageNumber, int pageSize)
        {
            return await repository.Search(userId, recipientId, pageNumber, pageSize);
        }

    }
}
