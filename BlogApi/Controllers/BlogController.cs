using BLOGAPI.Services;
using BLOGAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class BlogController : ControllerBase{
        private readonly IBlogService? _blogService;
        public BlogController(IBlogService blogService){
            _blogService = blogService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Blog>>> GetBlogs([FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 20){
            var blogs = await _blogService?.GetBlogs(pageNumber, pageSize)!;
            return Ok(blogs);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBlog(string id){
            try
            {
                var blog = await _blogService?.GetBlog(id)!;
                if (blog != null)
                {
                    return Ok(blog);
                }
                return NotFound($"There is No blog with id => {id}");
            }
            catch (InvalidInputException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateBlog(Blog blog){
            if(await _blogService?.CreateBlog(blog)! == true){
                return Ok("Blog Created Successfully");
            }
            return BadRequest("Failed to create Blog");
        }
        // public async Task<ActionResult> CreateBlog(Blog blog){
        //     if(await _blogService?.CreateBlog(blog)! == true){
        //         return Ok("Blog Created Successfully");
        //     }
        //     return BadRequest("Failed to create Blog");
        // }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBlog(string id, Blog blog){
            try
            {
                if (await _blogService?.UpdateBlog(id, blog)! == true)
                {
                    return Ok("Blog Updated Successfully");
                }
                return NotFound($"There is No blog with id => {id}");
            }
            catch (InvalidInputException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlog(string id){
            try
            {
                if (await _blogService?.DeleteBlog(id)! == true)
                {
                    return Ok("Blog Deleted Successfully");
                }
                return NotFound($"There is No blog with id => {id}");
            }
            catch (InvalidInputException e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("searchBlogs")]
        public async Task<ActionResult> SearchBlogs([FromQuery]string? title, [FromQuery]string? author){
            var blogs = await _blogService?.SearchBlogs(title, author)!;
            if(blogs.Count > 0){
                return Ok(blogs);
            }
            return NotFound("No Blogs Found");
        }
    }
}