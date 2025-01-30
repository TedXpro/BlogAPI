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
            if (mongoDBSettings == null || string.IsNullOrEmpty(mongoDBSettings.Value?.ConnectionString))
            {
                throw new ArgumentException("MongoDB connection string is not configured properly");
            }

            try
            {
                var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
                Console.WriteLine(mongoDBSettings.Value.ConnectionString);
                var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
                _comments = mongoDatabase.GetCollection<Comment>(mongoDBSettings.Value.CommentCollectionName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initialize MongoDB connection: {ex.Message}");
            }
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            comment.Id = null;
            comment.CreatedAt = DateTime.UtcNow;
            await _comments.InsertOneAsync(comment);
            return comment;
        }

        public async Task<Comment> GetByIdAsync(string id)
        {
            try{
            return await _comments.Find(c => c.Id == id).FirstOrDefaultAsync();
            } catch (FormatException){
                throw new InvalidInputException("Invalid id format " + id);
            }   
        }

        public async Task<List<Comment>> GetByBlogIdAsync(string blogId, int page = 1, int pageSize = 10) 
        { 
            if (string.IsNullOrWhiteSpace(blogId)) 
            { 
                throw new ArgumentException("Blog ID cannot be null or empty."); 
            } 
 
            try 
            { 
                var filter = Builders<Comment>.Filter.Eq(c => c.BlogId, blogId); 
 
                return await _comments.Find(filter) 
                                      .SortByDescending(c => c.CreatedAt)  // Sort newest comments first 
                                      .Skip((page - 1) * pageSize)  // Pagination: skip previous pages 
                                      .Limit(pageSize)  // Limit results per page 
                                      .ToListAsync(); 
            } 
            catch (FormatException) 
            { 
                throw new InvalidInputException($"Invalid Blog ID format: {blogId}"); 
            } 
            catch (Exception ex) 
            { 
                throw new Exception($"Error retrieving comments for Blog ID {blogId}: {ex.Message}", ex); 
            } 
        }
        public async Task<Comment> UpdateAsync(string id, Comment comment)
        {
            try{
            await _comments.ReplaceOneAsync(c => c.Id == id, comment);
            return comment;
            } catch (FormatException){
                throw new InvalidInputException("Invalid id format " + id);
            }
        }

        public async Task DeleteAsync(string id)
        {
            try{
            await _comments.DeleteOneAsync(c => c.Id == id);
            } catch (FormatException){
                throw new InvalidInputException("Invalid id format " + id);
            }
        }

        public async Task<long> GetCommentCountForBlogAsync(string blogId)
        {
            try{
            return await _comments.CountDocumentsAsync(c => c.BlogId == blogId);
            } catch (FormatException){
                throw new InvalidInputException("Invalid blog id format " + blogId);
            }
        }
    }
}
