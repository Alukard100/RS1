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

        [HttpDelete]
        [Route("DeleteUsedPromoCodes")]
        public IActionResult DeleteUsedPromoCodes()
        {
            try
            {
                var response = _service.DeleteUsedPromoCodes();
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetListOfActiveCodes")]
        public IActionResult GetListOfActiveCodes([FromQuery] GetListOfActiveCodesRequest request)
        {
            try
            {
                var response = _service.GetListOfActiveCodes(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
