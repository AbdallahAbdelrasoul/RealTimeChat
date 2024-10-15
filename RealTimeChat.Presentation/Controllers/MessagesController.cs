using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Application.Messages;
using RealTimeChat.Application.Messages.DTOs;
using RealTimeChat.Domain.Shared.Pagination;

namespace RealTimeChat.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesQueryService _messagesQueryService;
        private readonly IMessagesService _messagesService;
        public MessagesController(IMessagesQueryService messagesQueryService, IMessagesService messagesService)
        {
            _messagesQueryService = messagesQueryService;
            _messagesService = messagesService;
        }

        [HttpPost]
        public async Task<CreatedResult> Create([FromBody] MessageCreateDTO request)
        {
            var msg = await _messagesService.Create(request);
            return Created("api/messages/" + msg.Id, msg);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<MessageDTO>>> ListMessageHistory([FromQuery] MessageListHistoryDTO request)
        {
            return Ok(await _messagesQueryService.ListMessageHistory(request));
        }

        [HttpPatch("[Action]")]
        public async Task<ActionResult> Seen([FromBody] List<int> MessagesIds)
        {
            await _messagesService.MarkMessagesAsSeen(MessagesIds);
            return Ok();
        }

    }
}
