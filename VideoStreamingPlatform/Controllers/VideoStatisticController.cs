using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoStatisticController : Controller
    {
        private readonly IVideoStatisticService _service;
        public VideoStatisticController(IVideoStatisticService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetVideoStatistic")]
        public IActionResult GetVideoStatistic(int id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("Statistic doesnt exist");
            }
            var videoStatistic = _service.GetStatistic(id);
            return Ok(videoStatistic);
        }
    }
}
