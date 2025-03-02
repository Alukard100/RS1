using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Report;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        IReportService service;

        public ReportController(IReportService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("CreateReport")]
        public IActionResult CreateReport([FromBody] CreateReportRequest request)
        {
            try
            {
                var response = service.CreateReport(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetReports")]
        public IActionResult GetReports([FromQuery] GetReportsRequest request)
        {
            try
            {
                var response = service.GetReports(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateReport")]
        public IActionResult UpdateReport([FromBody] UpdateReportRequest request)
        {
            try
            {
                var response = service.UpdateReport(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteReport")]
        public IActionResult DeleteReport([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = service.DeleteReport(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}