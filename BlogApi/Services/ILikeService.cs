using BLOGAPI.Models; 
 
namespace BLOGAPI.Services{ 
    public interface ILikeService{ 
        public Task<bool> LikeBlog(string blogId, string userId); 
        public Task<bool> DislikeBlog(string blogId, string userId); 
        public Task<int> GetLikeCount(string blogId); 
        public Task<int> GetDislikeCount(string blogId); 
    } 
}