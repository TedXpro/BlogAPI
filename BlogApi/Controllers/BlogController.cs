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
        public async Task<ActionResult<List<Blog>>> GetBlogs(){
            var blogs = await _blogService?.GetBlogs()!;
            return Ok(blogs);
        }

        // [HttpGet("{id}")]

    }
}