using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Reflection;
using VideoStreamingPlatform.Commons;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.PlaylistGroup;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaylistGroupController : ControllerBase
    {
        IPlaylistGroupService service;

        public PlaylistGroupController(IPlaylistGroupService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("CreatePlaylistGroup")]
        public IActionResult CreatePlaylistGroup([FromBody] CreatePlaylistGroupRequest request)
        {
            try
            {
                var response = service.CreatePlaylistGroup(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPlaylistGroups")]
        public IActionResult GetPlaylistGroups([FromQuery] GetPlaylistGroupsRequest request)
        {
            try
            {
                var response = service.GetPlaylistGroups(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePlaylistGroup")]
        public IActionResult UpdatePlaylistGroup([FromBody] UpdatePlaylistGroupRequest request)
        {
            try
            {
                var response = service.UpdatePlaylistGroup(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeletePlaylistGroup")]
        public IActionResult DeletePlaylistGroup([FromBody] int playlistId, int videoId)
        {
            try
            {
                var response = service.DeletePlaylistGroup(playlistId, videoId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}