using Microsoft.AspNetCore.SignalR;

namespace RealTimeChat.Presentation.Hubs
{

    //[Authorize]
    public class ChatHub : Hub
    {
        //private readonly IMessagesService _messagesService;

        //public ChatHub(IMessagesService messagesService)
        //{
        //    _messagesService = messagesService;
        //}

        public async Task SendMessage(string message)
        {
            var username = Context.User?.Identity?.Name;
            await Clients.Client("a").SendAsync("ReceiveMessage", message);
        }

    }

}
