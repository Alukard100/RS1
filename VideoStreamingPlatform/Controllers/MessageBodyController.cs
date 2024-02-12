using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.MessageBody;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageBodyController : ControllerBase
    {
        IMessageBodyService _service;

        public MessageBodyController(IMessageBodyService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("CreateMessageBody")]
        public IActionResult CreateMessageBody([FromBody]CreateMessageBodyRequest request)
        {
            try
            {
            var response = _service.CreateMessageBody(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteMessageBody")]
        public IActionResult DeleteMessageBody([FromBody]CommonDeleteRequest request)
        {
            try
            {
                var response = _service.DeleteMessageBody(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateMessageBody")]
        public IActionResult UpdateMessageBody([FromBody]UpdateMessageBodyRequest request)
        {
            try
            {
                var response = _service.UpdateMessageBody(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DODATI GET ZA PORUKE ZA USERE KOJI SE NALAZE ILI U RECIEVERU ILI U SENDERU

        [HttpGet]
        [Route("GetMessageBody")]
        public IActionResult GetMessageBody([FromQuery] GetMessageBodyRequest request)
        {
            try
            {
                var response = _service.GetMessageBody(request);
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
