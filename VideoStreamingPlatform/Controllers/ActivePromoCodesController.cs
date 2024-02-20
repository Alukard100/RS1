using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests.ActivePromoCodes;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
        [ApiController]
        [Route("[controller]")]
    public class ActivePromoCodesController : ControllerBase
    {
        IActivePromoCodesService _service;

        public ActivePromoCodesController(IActivePromoCodesService service)
        {
            this._service= service;
        }

        [HttpPost]
        [Route("GeneratePromoCodes")]

        public IActionResult GeneratePromoCodes([FromBody] GeneratePromoCodesRequest request)
        {
            try
            {
                var response = _service.GeneratePromoCodes(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
