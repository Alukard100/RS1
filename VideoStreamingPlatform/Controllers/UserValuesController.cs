using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.UserValues;
using VideoStreamingPlatform.Commons.DTOs.Responses.UserValues;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserValuesController : ControllerBase
    {
        IUserValuesService service;
        public UserValuesController(IUserValuesService service)
        {
            this.service = service;
        }

        //Zavrsiti
        [HttpGet]
        [Route("GetUserValues")]
        public ActionResult<GetUserValuesResponse> GetUserValues([FromQuery] GetUserValuesRequest request)
        {
            try
            {
                var response = service.GetUserValues(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPut]
        [Route("UpdateUserValues")]
        public IActionResult UpdateUserValues([FromBody] UpdateUserValuesRequest request)
        {
            try
            {
                var response = service.UpdateUserValues(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // metoda ce se pozivati istovremeno kada i metoda deleteUser iz controllera UserController
        [HttpDelete]
        [Route("DeleteUserValues")]
        public IActionResult CreateUserValues([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = service.DeleteUserValues(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateUserValues")]
        public IActionResult CreateUserValues([FromBody]CreateUserValuesRequest request)
        {
            try
            {
                var response = service.CreateUserValues(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
