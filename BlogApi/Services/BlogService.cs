using BLOGAPI.Models;
using MongoDB.Driver;

namespace BlogApi.Services
{
    public class BlogService : IBlogService
    {
        private readonly IMongoCollection<Blog>? _blogs;
        public BlogService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MonogoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["MonogoDbSettings:DatabaseName"]);
            _blogs = database.GetCollection<Blog>(configuration["MonogoDbSettings:BlogCollectionName"]);
        }

        public async Task<List<Blog>> GetBlogs(int pageNumber, int pageSize)
        {
            try
            {
                return await _blogs.Find(_ => true)
                    .Skip((pageNumber - 1) * pageSize)
                    .Limit(pageSize)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to retrieve blogs", e);
            }
        }

        public async Task<Blog> GetBlog(string id)
        {
            var blog = await _blogs.Find(b => b.Id == id).FirstOrDefaultAsync();
            return blog;
        }

        public async Task<bool> CreateBlog(Blog blog)
        {
            blog.Id = null;
            await _blogs?.InsertOneAsync(blog)!;
            return true;
        }

        public async Task<bool> UpdateBlog(string id, Blog blog)
        {
            blog.Id = id;
            var status = await _blogs.ReplaceOneAsync(b => b.Id == id, blog);
            if (status.ModifiedCount > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteBlog(string id)
        {
            var status = await _blogs.DeleteOneAsync(b => b.Id == id);
            if (status.DeletedCount > 0)
            {
                return true;
            }
            return false;
        }
    }
}