using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Reflection;
using VideoStreamingPlatform.Commons;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.GroupMembers;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupMembersController : ControllerBase
    {
        IGroupMemberService service;

        public GroupMembersController(IGroupMemberService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("CreateGroupMembers")]
        public IActionResult CreateGroupMembers([FromBody] CreateGroupMembersRequest request)
        {
            try
            {
                var response = service.CreateGroupMembers(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetGroupMembers")]
        public IActionResult GetGroupMembers([FromQuery] GetGroupMembersRequest request)
        {
            try
            {
                var response = service.GetGroupMembers(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateGroupMembers")]
        public IActionResult UpdateGroupMembers([FromBody] UpdateGroupMembersRequest request)
        {
            try
            {
                var response = service.UpdateGroupMembers(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteGroupMembers")]
        public IActionResult DeleteGroupMembers([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = service.DeleteGroupMembers(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}