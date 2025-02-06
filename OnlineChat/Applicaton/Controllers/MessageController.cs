using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineChat.Applicaton.DTO_s;
using OnlineChat.Core.Abstactions;
using OnlineChat.Core.DTO_s;
using OnlineChat.Core.Services;

namespace OnlineChat.Applicaton.Controllers
{
    [Route("messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageController(IMessageService service, IHubContext<MessageHub> hubContext)
        {
            _messageService = service;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessage([FromBody] MessageAddDTO message)
        {
            var reuslt = await _messageService.SaveMessage(message);
            if (reuslt.IsFailure) return BadRequest(new { error = reuslt.Error });
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.Content);
            return Ok(message);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessage(DateTime startPoint, DateTime endPoint)
        {
            var result = await _messageService.GetLastMessages(startPoint, endPoint);
            if (result.IsFailure) return BadRequest(new { error = result.Error });
            return Ok(result.Value);
        }

    }
}
