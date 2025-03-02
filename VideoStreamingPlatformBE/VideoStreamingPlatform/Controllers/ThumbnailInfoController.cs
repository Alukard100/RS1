using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests.ThumbnailInfo;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ThumbnailInfoController : Controller
    {
        private readonly IThumbnailInfoService _service;
        public ThumbnailInfoController(IThumbnailInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetThumbnailInfo")]
        public IActionResult GetThumbnailInfo(int id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("Thumbnail doesnt exist");
            }
            var thumbnail = _service.GetThumbnail(id);
            return Ok(thumbnail);
        }

        [HttpPatch]
        [Route("UpdateThumbnailInfo")]
        public IActionResult UpdateThumbnail(UpdateThumbnailInfoRequest request)
        {
            if (request == null || request.file == null) 
            {
                return BadRequest("Something is wrong with the picture or video you are trying to update"); ;
            }
            var thumbnail = _service.UpdateThumbnail(request);
            return Ok(thumbnail);
        }
    }
}
