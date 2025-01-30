using Microsoft.AspNetCore.Mvc;
using BLOGAPI.Models;
using BLOGAPI.Services;

namespace BLOGAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("post")]
        public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment comment)
        {
            if (comment == null) return BadRequest();
            
            var createdComment = await _commentService.CreateAsync(comment);
            return CreatedAtAction(nameof(GetComment), new { id = createdComment.Id }, createdComment);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<Comment>> EditComment(string id, [FromBody] Comment comment)
        {
            try{
            if (comment == null) return BadRequest();

            var existingComment = await _commentService.GetByIdAsync(id);
            if (existingComment == null) return NotFound();

            comment.Id = id;
            var updatedComment = await _commentService.UpdateAsync(id, comment);
            return Ok(updatedComment);
            } catch (InvalidInputException ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            try{
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null) return NotFound();

            await _commentService.DeleteAsync(id);
            return NoContent();
            } catch (InvalidInputException ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(string id)
        {
            try{
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment);
            } catch (InvalidInputException ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("for/{blogId}")]
        public async Task<ActionResult<CommentResponse>> GetCommentsForBlog(string blogId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try{
            var comments = await _commentService.GetByBlogIdAsync(blogId, page, pageSize);
            var totalComments = await _commentService.GetCommentCountForBlogAsync(blogId);
            
            var response = new CommentResponse
            {
                Comments = comments,
                TotalComments = totalComments,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalComments / (double)pageSize)
            };
            
            return Ok(response);
            } catch (InvalidInputException ex){
                return BadRequest(ex.Message);
            }
        }
    }
}