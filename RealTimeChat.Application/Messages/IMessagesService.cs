using RealTimeChat.Application.Messages.DTOs;

namespace RealTimeChat.Application.Messages
{
    public interface IMessagesService
    {
        Task<MessageDTO> Create(MessageCreateDTO input);
    }
}
