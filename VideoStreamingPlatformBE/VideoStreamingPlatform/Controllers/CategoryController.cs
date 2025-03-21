using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Category;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("CreateCategory")]
        [Authorize(Roles = "Admin")]
        public IActionResult create(CreateCategoryRequest request)
        {
            var category = _service.CreateCategory(request.CategoryName);

            return Ok(category);
        }
        [HttpDelete]
        [Route("DeleteCategory")]
        [Authorize(Roles = "Admin")]
        public IActionResult delete(CommonDeleteRequest request)
        {
            var category = _service.DeleteCategory(request.Id);
            return Ok(category);
        }
        [HttpGet]
        [Route("GetCategory")]
        public IActionResult get([FromQuery] GetCategoryRequest request) 
        {
            try
            {
                var category = _service.GetCategory(request.CategoryId);
                return Ok(category);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }    
        }
        [HttpPut]
        [Route("UpdateCategory")]
        public IActionResult update(UpdateCategoryRequest request) 
        {
            try
            {
                var category = _service.UpdateCategory(request);
                return Ok(category);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }

}
