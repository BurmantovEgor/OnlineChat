using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineChat.Applicaton.DTO_s;
using OnlineChat.Core.Abstactions;
using OnlineChat.Core.Services;

namespace OnlineChat.Applicaton.Controllers
{
    [Route("messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService service)
        {
            _messageService = service;
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessage([FromBody] MessageAddDTO message)
        {
            var reuslt = await _messageService.SaveMessage(message);
            if (reuslt.IsFailure) return BadRequest(new { error = reuslt.Error });
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
