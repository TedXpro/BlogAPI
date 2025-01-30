using BLOGAPI.Models;

namespace BLOGAPI.Services{
    public interface IBlogService{
        public Task<List<Blog>> GetBlogs(int pageNumber, int pageSize);
        public Task<Blog> GetBlog(string id);
        public Task<bool> CreateBlog(Blog blog);
        public Task<bool> UpdateBlog(string id, Blog blog);
        public Task<bool> DeleteBlog(string id);
        public Task<List<Blog>> SearchBlogs(string? title, string? author);
    }
}