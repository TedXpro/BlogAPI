using BLOGAPI.Models;
using MongoDB.Driver;

namespace BlogApi.Services{
    public class BlogService : IBlogService{
        private readonly IMongoCollection<Blog>? _blogs;
        public BlogService(IConfiguration configuration){
            var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
            _blogs = database.GetCollection<Blog>(configuration["MongoDbSettings:BlogCollectionName"]); 
        }

        public async Task<List<Blog>> GetBlogs() {
            var blogs = await _blogs.Find(_ => true).ToListAsync();
            return blogs;
        }

        public async Task<Blog> GetBlog(string id){
            var blog = await _blogs.Find(b => b.Id == id).FirstOrDefaultAsync();
            return blog;
        }
            
        public async Task<bool> CreateBlog(Blog blog){
            await _blogs?.InsertOneAsync(blog)!;
            return true;
        }

        public async Task<bool> UpdateBlog(string id, Blog blog){
            var status = await _blogs.ReplaceOneAsync(b => b.Id == id, blog);
            if(status.ModifiedCount > 0){
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteBlog(string id){
            var status = await _blogs.DeleteOneAsync(b => b.Id == id);
            if(status.DeletedCount > 0){
                return true;
            }
            return false;
        }
    }
}