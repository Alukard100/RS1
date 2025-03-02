using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons;
using VideoStreamingPlatform.Database.Models;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Commons.DTOs.Requests.User;
using VideoStreamingPlatform.Commons.DTOs.Requests;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {

        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers([FromQuery] GetUserRequest request)
        {
            try
            {
            var response = _userService.GetUser(request);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
       
        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var response = _userService.CreateUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("DeleteUser")]
        public IActionResult DeleteUser([FromBody]CommonDeleteRequest request)
        {
            try
            {
                var response = _userService.DeleteUser(request);
                return Ok (response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest request)
        {
            try
            {
                var response = _userService.UpdateUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        //[HttpGet]
        //[Route("GetUsers")]

        //public List<AdType> GetAdType()
        //{
        //    var response = new List<AdType>();
        //    response.Add(new AdType() { Id = 1, Name = "admin" });
        //    response.Add(new AdType() { Id = 2, Name = "super-admin" });
        //    response.Add(new AdType() { Id = 3, Name = "guest" });

        //    return response;
        //}

    }
}