using BLOGAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BLOGAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMongoCollection<Comment> _comments;

        public CommentService(IOptions<MongoDbSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _comments = mongoDatabase.GetCollection<Comment>(mongoDBSettings.Value.CommentCollectionName);
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            comment.CreatedAt = DateTime.UtcNow;
            await _comments.InsertOneAsync(comment);
            return comment;
        }

        public async Task<Comment> GetByIdAsync(string id)
        {
            return await _comments.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Comment>> GetByBlogIdAsync(string blogId, int page = 1, int pageSize = 10)
        {
            return await _comments.Find(c => c.BlogId == blogId)
                                .SortByDescending(c => c.CreatedAt)
                                .Skip((page - 1) * pageSize)
                                .Limit(pageSize)
                                .ToListAsync();
        }

        public async Task<Comment> UpdateAsync(string id, Comment comment)
        {
            await _comments.ReplaceOneAsync(c => c.Id == id, comment);
            return comment;
        }

        public async Task DeleteAsync(string id)
        {
            await _comments.DeleteOneAsync(c => c.Id == id);
        }

        public async Task<long> GetCommentCountForBlogAsync(string blogId)
        {
            return await _comments.CountDocumentsAsync(c => c.BlogId == blogId);
        }
    }
}
