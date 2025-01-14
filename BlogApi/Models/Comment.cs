using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BLOGAPI.Models{
    public class Comment{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id{get; set;}
        public string Body{get; set;}
        public DateTime CreatedAt{get; set;}
        public string AuthorName{get; set;}
        public string AuthorId{get; set;}
        public string BlogId{get; set;}

    }
}