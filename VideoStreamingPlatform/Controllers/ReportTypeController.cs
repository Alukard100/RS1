using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Reflection;
using VideoStreamingPlatform.Commons;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.ReportType;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportTypeController : ControllerBase
    {
        IReportTypeService service;

        public ReportTypeController(IReportTypeService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("CreateReportType")]
        public IActionResult CreateReportType([FromBody] CreateReportTypeRequest request)
        {
            try
            {
                var response = service.CreateReportType(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetReportTypes")]
        public IActionResult GetReportTypes([FromQuery] GetReportTypesRequest request)
        {
            try
            {
                var response = service.GetReportTypes(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateReportType")]
        public IActionResult UpdateReportType([FromBody] UpdateReportTypeRequest request)
        {
            try
            {
                var response = service.UpdateReportType(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteReportType")]
        public IActionResult DeleteReportType([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = service.DeleteReportType(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}