using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NotificationTypeController : ControllerBase
    {
        INotificationTypeService _service;

        public NotificationTypeController(INotificationTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetNotificationType")]
        public IActionResult GetNotificationType([FromQuery] CommonDeleteRequest request)
        {
            try
            {
                var response = _service.GetNotificationType(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
