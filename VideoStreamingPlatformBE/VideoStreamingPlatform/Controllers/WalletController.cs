using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests.Wallet;
using VideoStreamingPlatform.Commons.DTOs.Responses.Wallet;
using VideoStreamingPlatform.Commons.Interfaces;


namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        IWalletService _service;
        public WalletController(IWalletService service)
        {
            _service = service;
        }
        

        [HttpPut]
        [Route("UpdateWallet")]
        public IActionResult UpdateWallet([FromBody]UpdateWalletRequest request)
        {
            try
            {
                var response = _service.UpdateWallet(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("EnterPromoCode")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult EnterPromoCode([FromBody] EnterPromoCodeRequest request)
        {
            try
            {
                var response = _service.EnterPromoCode(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);   
            }
        }

        [HttpGet("GetWallet")]
        public ActionResult<GetWalletResponse> GetWallet([FromQuery]GetWalletRequest request)
        {
            try
            {
            GetWalletResponse response = _service.GetWallet(request);
            return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
