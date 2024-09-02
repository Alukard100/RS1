using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Advertisement;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllerss
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertisementController : ControllerBase
    {
        IAdvertisementService service;

        public AdvertisementController(IAdvertisementService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("CreateAdvertisement")]
        public IActionResult CreateAdvertisement([FromBody] CreateAdvertisementRequest request)
        {
            try 
            {
                var response = service.CreateAdvertisement(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAdvertisements")]
        public IActionResult GetAdvertisements([FromQuery] GetAdvertisementsRequest request)
        {
            try
            {
                var response = service.GetAdvertisements(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateAdvertisement")]
        public IActionResult UpdateAdvertisement([FromBody] UpdateAdvertisementRequest request)
        {
            try
            {
                var response = service.UpdateAdvertisement(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteAdvertisement")]
        public IActionResult DeleteAdvertisement([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = service.DeleteAdvertisement(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}