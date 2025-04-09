using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.MessageBody;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Hubs;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageBodyController : ControllerBase
    {
        private readonly IMessageBodyService _service;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageBodyController(IMessageBodyService service, IHubContext<ChatHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("CreateMessageBody")]
        public async Task<IActionResult> CreateMessageBody([FromBody] CreateMessageBodyRequest request)
        {
            try
            {
                var response = await _service.CreateMessageBody(request);

                await _hubContext.Clients.User(request.MsgRecieverId.ToString())
    .SendAsync("ReceiveMessage", new { senderId = request.MsgSenderId, body = request.Body, timeSent = DateTime.UtcNow });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteMessageBody")]
        public async Task<IActionResult> DeleteMessageBody([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = await _service.DeleteMessageBody(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateMessageBody")]
        public async Task<IActionResult> UpdateMessageBody([FromBody] UpdateMessageBodyRequest request)
        {
            try
            {
                var response = await _service.UpdateMessageBody(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetMessageBody")]
        public async Task<IActionResult> GetMessageBody([FromQuery] GetMessageBodyRequest request)
        {
            try
            {
                var response = await _service.GetMessageBody(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
