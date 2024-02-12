using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.User;
using VideoStreamingPlatform.Commons.DTOs.Requests.UserType;
using VideoStreamingPlatform.Commons.DTOs.Responses.UserType;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserTypeController : ControllerBase
    {
        IUserTypeService _service;

        public UserTypeController(IUserTypeService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("CreateUserType")]
        public IActionResult CreateUserType([FromBody] CreateUserTypeRequest request)
        {
            try
            {
                var response = _service.CreateUserType(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteUserType")]
        public IActionResult DeleteUserType([FromBody]CommonDeleteRequest request)
        {
            try
            {
                var response = _service.DeleteUserType(request);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserTypes")]
        public IActionResult GetUserType()
        {
            try
            {
                IEnumerable<GetUserTypeResponse> response = _service.GetUserType();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}