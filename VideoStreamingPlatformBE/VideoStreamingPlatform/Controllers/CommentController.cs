using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Comment;
using VideoStreamingPlatform.Commons.Interfaces;

namespace VideoStreamingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _service;
        public CommentController(ICommentService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("CreateComment")]
        public IActionResult CreateComment(CreateCommentRequest request)
        {
            if (request == null)
            {
                return BadRequest("There is something wrong with your comment buddy, go fix it");
            }
            var comment = _service.CreateComment(request);
            return Ok(comment);
        }

        [HttpDelete]
        [Route("DeleteComment")]
        public IActionResult DeleteComment(CommonDeleteRequest request)
        {
            if (request.Id > 0)
            {
                var comment = _service.DeleteComment(request);
                return Ok(comment);
            } return BadRequest("This Comment doesn't exist my boi");
        }

        [HttpGet]
        [Route("GetComment")]
        public IActionResult GetComment(int id)
        {
            if (id > 0)
            {
                var comment = _service.GetComment(id);
                return Ok(comment);
            } return BadRequest("You are attempting to get something that isnt there");
        }

        [HttpPatch]
        [Route("UpdateComment")]
        public IActionResult UpdateComment(UpdateCommentRequest request) 
        {
            if (request == null)
            {
                return BadRequest("Comment failed to upload");
            }
            var comment = _service.UpdateComment(request);
            return Ok(comment);
        }
    }
}
