using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Reflection;
using VideoStreamingPlatform.Commons;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Synchronization;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SynchronizationController : ControllerBase
    {
        ISynchronizationService service;

        public SynchronizationController(ISynchronizationService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("CreateSynchronization")]
        public IActionResult CreateSynchronization([FromBody] CreateSynchronizationRequest request)
        {
            try
            {
                var response = service.CreateSynchronization(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetSynchronizations")]
        public IActionResult GetSynchronizations([FromQuery] GetSynchronizationsRequest request)
        {
            try
            {
                var response = service.GetSynchronizations(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateSynchronization")]
        public IActionResult UpdateSynchronization([FromBody] UpdateSynchronizationRequest request)
        {
            try
            {
                var response = service.UpdateSynchronization(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteSynchronization")]
        public IActionResult DeleteSynchronization([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = service.DeleteSynchronization(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}