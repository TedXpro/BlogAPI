using BLOGAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
 
namespace BlogApi.Controllers 
{ 
    [Authorize]
    [ApiController] 
    [Route("[Controller]")] 
    public class LikeController : ControllerBase 
    { 
        private readonly ILikeService _likeService; 
        public LikeController(ILikeService likeService) 
        { 
            _likeService = likeService; 
        } 

        [Authorize]
        [HttpPost("like")] 
        public async Task<IActionResult> LikeBlog(string blogId, [FromQuery] string userId) 
        { 
            if (string.IsNullOrWhiteSpace(blogId) || string.IsNullOrWhiteSpace(userId)) 
            { 
                return BadRequest(new { message = "Blog ID and User ID cannot be empty." }); 
            } 
 
            bool success = await _likeService.LikeBlog(blogId, userId); 
            if (!success) 
            { 
                return BadRequest(new { message = "You have already liked this blog." }); 
            } 
 
            return Ok(new { message = "Blog liked successfully!" }); 
        } 

        [Authorize]
        [HttpGet("likesCount")] 
        public async Task<IActionResult> GetLikeCount(string blogId) 
        { 
            if (string.IsNullOrWhiteSpace(blogId)) 
            { 
                return BadRequest(new { message = "Blog ID cannot be empty." }); 
            } 
 
            int count = await _likeService.GetLikeCount(blogId); 
            return Ok(new { blogId, likeCount = count }); 
        } 
 
        [Authorize]
        [HttpPost("{blogId}/dislike")] 
        public async Task<ActionResult> DislikeBlog(string blogId, [FromQuery] string userId) 
        { 
            if (await _likeService.DislikeBlog(blogId, userId)) 
            { 
                return Ok("Blog Disliked Successfully"); 
            } 
            return BadRequest("You have already disliked this blog"); 
        } 

        [Authorize]
        [HttpGet("{blogId}/dislikesCount")] 
        public async Task<IActionResult> GetDislikeCount(string blogId) 
        { 
            if (string.IsNullOrWhiteSpace(blogId)) 
            { 
                return BadRequest(new { message = "Blog ID cannot be empty." }); 
            } 
 
            var count = await _likeService.GetDislikeCount(blogId); 
            return Ok(new { blogId, dislikeCount = count }); 
        } 
    } 
}