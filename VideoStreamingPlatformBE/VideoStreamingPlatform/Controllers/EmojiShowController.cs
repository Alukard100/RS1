using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.EmojiShow;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class EmojiShowController : ControllerBase
    {
        IEmojiShowService service;

        public EmojiShowController(IEmojiShowService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("CreateEmojiShow")]
        public IActionResult CreateEmojiShow([FromBody] CreateEmojiShowRequest request)
        {
            try
            {
                var response = service.CreateEmojiShow(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetEmojiShows")]
        public IActionResult GetEmojiShows([FromQuery] GetEmojiShowsRequest request)
        {
            try
            {
                var response = service.GetEmojiShows(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateEmojiShow")]
        public IActionResult UpdateEmojiShow([FromBody] UpdateEmojiShowRequest request)
        {
            try
            {
                var response = service.UpdateEmojiShow(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteEmojiShow")]
        public IActionResult DeleteEmojiShow([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = service.DeleteEmojiShow(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}