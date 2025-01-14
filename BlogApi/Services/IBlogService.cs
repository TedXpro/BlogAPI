using BLOGAPI.Models;

namespace BlogApi.Services{
    public interface IBlogService{
        public Task<List<Blog>> GetBlogs();
        public Task<Blog> GetBlog(string id);
        public Task<bool> CreateBlog(Blog blog);
        public Task<bool> UpdateBlog(string id, Blog blog);
        public Task<bool> DeleteBlog(string id);
    }
}