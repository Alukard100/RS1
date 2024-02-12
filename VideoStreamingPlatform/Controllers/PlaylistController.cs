using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Reflection;
using VideoStreamingPlatform.Commons;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Playlist;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaylistController : ControllerBase
    {
        IPlaylistService service;

        public PlaylistController(IPlaylistService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("CreatePlaylist")]
        public IActionResult CreatePlaylist([FromBody] CreatePlaylistRequest request)
        {
            try
            {
                var response = service.CreatePlaylist(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPlaylists")]
        public IActionResult GetPlaylists([FromQuery] GetPlaylistsRequest request)
        {
            try
            {
                var response = service.GetPlaylists(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePlaylist")]
        public IActionResult UpdatePlaylist([FromBody] UpdatePlaylistRequest request)
        {
            try
            {
                var response = service.UpdatePlaylist(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeletePlaylist")]
        public IActionResult DeletePlaylist([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = service.DeletePlaylist(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}