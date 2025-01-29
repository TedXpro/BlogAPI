namespace BLOGAPI.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string BlogCollectionName { get; set; } = string.Empty;
        public string CommentCollectionName { get; set; } = string.Empty;
    }
}
