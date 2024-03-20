using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Support;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
        [ApiController]
        [Route("[controller]")]
    public class SupportController : ControllerBase
    {
        ISupportService _service;
        public SupportController(ISupportService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("CreateSupport")]
        public IActionResult CreateSupport([FromBody] CreateSupportRequest request)
        {
            try
            {
                var response = _service.CreateSupport(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteSupport")]
        public IActionResult DeleteSupport([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = _service.DeleteSupport(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetSupport")]
        public IActionResult GetSupport([FromQuery] GetSupportRequest request)
        {
            try
            {
                var response = _service.GetSupport(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
