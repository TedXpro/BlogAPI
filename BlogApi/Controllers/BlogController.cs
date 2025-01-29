using BlogApi.Services;
using BLOGAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers{
    [ApiController]
    [Route("[Controller]")]
    public class BlogController : ControllerBase{
        private readonly IBlogService? _blogService;
        public BlogController(IBlogService blogService){
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Blog>>> GetBlogs([FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 20){
            var blogs = await _blogService?.GetBlogs(pageNumber, pageSize)!;
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBlog(string id){
            var blog = await _blogService?.GetBlog(id)!;
            if(blog != null){
                return Ok(blog);
            }
            return NotFound($"There is No blog with id => {id}");
        }

        [HttpPost]
        public async Task<ActionResult> CreateBlog(Blog blog){
            if(await _blogService?.CreateBlog(blog)! == true){
                return Ok("Blog Created Successfully");
            }
            return BadRequest("Failed to create Blog");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBlog(string id, Blog blog){
            if(await _blogService?.UpdateBlog(id, blog)! == true){
                return Ok("Blog Updated Successfully"); 
            }
            return NotFound($"There is No blog with id => {id}");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlog(string id){
            if(await _blogService?.DeleteBlog(id)! == true){
                return Ok("Blog Deleted Successfully");
            }
            return NotFound($"There is No blog with id => {id}");
        }
    }
}