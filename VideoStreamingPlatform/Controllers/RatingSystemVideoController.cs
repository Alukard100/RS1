using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingSystemVideoController : Controller
    {
        private readonly IRatingSystemVideoService _service;
        public RatingSystemVideoController(IRatingSystemVideoService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("GetRatingSystemVideo")]
        public IActionResult GetRSV(int id)
        {
            if (id <= 0 || id == null) 
            {
                return BadRequest("Rating system doesnt exist");
            }
            var ratingSystemVideo = _service.GetRSV(id);
            return Ok(ratingSystemVideo);
        }
    }
}
