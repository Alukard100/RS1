using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests.Wallet;
using VideoStreamingPlatform.Commons.DTOs.Responses;
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
        // Koristen trigger CreateWalletOnUserInsert u bazi podataka, koji se okida na stvaranje novog usera kojem dodjeljuje balance=0
        // te uzima njegov id, tako da cu praviti samo GET i PUT metodu

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
