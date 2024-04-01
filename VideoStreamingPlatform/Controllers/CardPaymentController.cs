using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests.CardPayment;
using VideoStreamingPlatform.Commons.DTOs.Responses.CardPayment;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
        [Route("[controller]")]
        [ApiController]
    public class CardPaymentController : ControllerBase
    {
        ICardPaymentService _service;

        public CardPaymentController(ICardPaymentService service)
        {
            _service = service;
        }

        // ZAVRSITI!!!
        // Pozivat ce se metoda editWallet iz WalletController-a za mijenjanje balance-a, ili cu ovde mijenjati stanje
        // Ubacit cu Stripe ili Paypal library za provjeru kartice
        // Za test samo pravim edit walleta
        [HttpPost]
        [Route("CreateCardPayment")]
        public IActionResult CreateCardPayment([FromBody] CreateCardPaymentRequest request)
        {
            try
            {
                var response = _service.CreateCardPayment(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetCardPayment")]
        public ActionResult<GetCardPaymentResponse> GetCardPayment([FromQuery] GetCardPaymentRequest request)
        {
            try
            {
                var response = _service.GetCardPayment(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
