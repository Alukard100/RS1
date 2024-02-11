using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Reflection;
using VideoStreamingPlatform.Commons;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Blog;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        IBlogService service;

        public BlogController(IBlogService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("CreateBlog")]
        public IActionResult CreateBlog([FromBody] CreateBlogRequest request)
        {
            try
            {
                var response = service.CreateBlog(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBlogs")]
        public IActionResult GetBlogs([FromQuery] GetBlogsRequest request)
        {
            try
            {
                var response = service.GetBlogs(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateBlog")]
        public IActionResult UpdateBlog([FromBody] UpdateBlogRequest request)
        {
            try
            {
                var response = service.UpdateBlog(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteBlog")]
        public IActionResult DeleteBlog([FromBody] CommonDeleteRequest request)
        {
            try
            {
                var response = service.DeleteBlog(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}