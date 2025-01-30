using BlogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class LikeController : ControllerBase{
        private readonly ILikeService _likeService;
        public LikeController(ILikeService likeService){
            _likeService = likeService;
        }

        [Authorize]
        [HttpPost("{blogId}/like")]
        public async Task<ActionResult> LikeBlog(string blogId, [FromQuery] string userId){
            if(await _likeService.LikeBlog(blogId, userId)){
                return Ok("Blog Liked Successfully");
            }
            return BadRequest("You have already liked this blog");
        }

        [Authorize]
        [HttpPost("{blogId}/dislike")]
        public async Task<ActionResult> DislikeBlog(string blogId, [FromQuery] string userId){
            if(await _likeService.DislikeBlog(blogId, userId)){
                return Ok("Blog Disliked Successfully");
            }
            return BadRequest("You have already disliked this blog");
        }

        [Authorize]
        [HttpGet("{blogId}/count")]
        public async Task<ActionResult<int>> GetLikeCount(string blogId){
            var count = await _likeService.GetLikeCount(blogId);
            return Ok(count);
        }
    }
}