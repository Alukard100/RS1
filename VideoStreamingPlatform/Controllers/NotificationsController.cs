using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Notifications;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
        [ApiController]
        [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {
        INotificationService _service;
        public NotificationsController(INotificationService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("CreateNotification")]
        public IActionResult CreateNotification([FromBody] CreateNotificationRequest request)
        {
            try
            {
                var response = _service.CreateNotification(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteNotification")]
        public IActionResult DeleteNotification([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = _service.DeleteNotification(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetNotifications")]
        public IActionResult GetNotification([FromQuery] GetNotificationsRequest request)
        {
            try
            {
                var response = _service.GetNotification(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
