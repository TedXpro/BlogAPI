using BLOGAPI.Models; 
using MongoDB.Driver; 
 
namespace BlogApi.Services{ 
    public class LikeService : ILikeService{ 
        private readonly IMongoCollection<Like>? _likes; 
        private readonly IMongoCollection<Blog>? _blogs; 
 
        public LikeService(IConfiguration configuration){ 
            var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]); 
            var database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]); 
            _likes = database.GetCollection<Like>(configuration["MongoDbSettings:LikeCollectionName"]); 
            _blogs = database.GetCollection<Blog>(configuration["MongoDbSettings:BlogCollectionName"]); 
        } 
 
        public async Task<bool> LikeBlog(string blogId, string userId) 
        { 
            var existingLike = await _likes.Find(l => l.BlogId == blogId && l.UserId == userId).FirstOrDefaultAsync(); 
            if (existingLike != null && existingLike.Type == "like") return false; 
 
            if (existingLike != null && existingLike.Type == "dislike") 
            { 
                await _likes.DeleteOneAsync(l => l.Id == existingLike.Id); 
            } 
 
            var like = new Like 
            { 
                BlogId = blogId, 
                UserId = userId, 
                Type = "like" 
            }; 
            await _likes?.InsertOneAsync(like)!; 
 
            var filter = Builders<Blog>.Filter.Eq(b => b.Id, blogId); 
            var update = Builders<Blog>.Update.Inc(b => b.LikeCount, 1); 
            await _blogs?.UpdateOneAsync(filter, update)!; 
 
            return true; 
        } 
 
        public async Task<int> GetLikeCount(string blogId) 
        { 
            return (int)await _likes.CountDocumentsAsync(l => l.BlogId == blogId && l.Type == "like"); 
        } 
 
        public async Task<bool> DislikeBlog(string blogId, string userId){ 
            var existingLike = await _likes.Find(l => l.BlogId == blogId && l.UserId == userId).FirstOrDefaultAsync(); 
            if (existingLike != null && existingLike.Type == "dislike") return false; 
 
            if (existingLike != null && existingLike.Type == "like"){ 
                await _likes.DeleteOneAsync(l => l.Id == existingLike.Id); 
            } 
 
            var dislike = new Like { 
                BlogId = blogId, 
                UserId = userId, 
                Type = "dislike" 
            }; 
            await _likes?.InsertOneAsync(dislike)!; 
 
            var filter = Builders<Blog>.Filter.Eq(b => b.Id, blogId); 
            var update = Builders<Blog>.Update.Inc(b => b.LikeCount, -1); 
            await _blogs?.UpdateOneAsync(filter, update)!; 
 
            return true; 
        } 
 
        public async Task<int> GetDislikeCount(string blogId){ 
            return (int)await _likes.CountDocumentsAsync(l => l.BlogId == blogId && l.Type == "dislike"); 
        } 
} 
}