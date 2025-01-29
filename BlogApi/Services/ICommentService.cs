using BLOGAPI.Models;

namespace BLOGAPI.Services
{
    public interface ICommentService
    {
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment> GetByIdAsync(string id);
        Task<List<Comment>> GetByBlogIdAsync(string blogId, int page = 1, int pageSize = 10);
        Task<Comment> UpdateAsync(string id, Comment comment);
        Task DeleteAsync(string id);
        Task<long> GetCommentCountForBlogAsync(string blogId);
    }
}