using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Membership;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
        [Route("[controller]")]
    public class MembershipController : ControllerBase
    {
        IMembershipService _service;
        public MembershipController(IMembershipService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("CreateMembership")]
        public IActionResult CreateMembership([FromBody] CreateMembershipRequest request)
        {
            try
            {
                var response = _service.CreateMembership(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteMembership")]
        public IActionResult DeleteMembership([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = _service.DeleteMembership(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdateMembership")]
        public IActionResult UpdateMembership([FromBody] CreateMembershipRequest request)
        {
            try
            {
                var response = _service.UpdateMembership(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetMembership")]
        public IActionResult GetMembership([FromQuery] GetMembershipRequest request)
        {
            try
            {
            var response = _service.GetMembership(request);
            return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
