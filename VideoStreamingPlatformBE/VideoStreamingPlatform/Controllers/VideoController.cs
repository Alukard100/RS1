using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Video;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : Controller
    {
        private readonly IVideoService _service;
        private readonly VideoSettings _videoSettings;
        public VideoController(IVideoService service, IOptions<VideoSettings> videoSettings)
        {
            _service = service;
            _videoSettings = videoSettings.Value;
        }

        [HttpPost]
        [Route("CreateVideo")]
        public IActionResult CreateVideo([FromForm] CreateVideoRequest request)
        {

            if (request.file == null || request.file.Length == 0)
            {
                return BadRequest("Video file not provided or is empty.");
            }
            string videoDirectory = _videoSettings.VideoDirectory;
            var video = _service.CreateVideo(request, HttpContext);

            return Ok(video);
        }
        [HttpDelete]
        [Route("DeleteVideo")]
        public IActionResult DeleteVideo(CommonDeleteRequest request)
        {
            if (request == null)
            {
                return BadRequest("Selected video does not exist");
            }
            var video = _service.DeleteVideo(request.Id);
            return Ok(video);
        }
        [HttpGet]
        [Route("GetVideo")]
        public IActionResult GetVideo(int id)
        {
            if (id == null)
            {
                return BadRequest("Video doesn't exist");
            }
            var video = _service.GetVideo(id);
            if (video == null)
            {
                return BadRequest("Video doesn't exist");
            }
            return Ok(video);
        }

        [HttpGet("stream/{videoId}")]
        public IActionResult StreamVideo(int videoId)
        {
            var stream = _service.StreamVideo(videoId);
            if (stream == null)
            {
                return NotFound();
            }
            return File(stream, "video/mp4", enableRangeProcessing: true);
        }

        [HttpPatch]
        [Route("UpdateVideo")]
        public IActionResult UpdateVideo(UpdateVideoRequest request)
        {
            if (request == null)
            {
                return BadRequest("Video you are trying to update is none existant");
            }
            var video = _service.UpdateVideo(request);
            return Ok(video);
        }
    }
}
