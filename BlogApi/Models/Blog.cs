using Microsoft.AspNetCore.Components.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BLOGAPI.Models{
    public class Blog{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id{get; set;}
        public string? Title{get; set;}
        public string? Body{get; set;}
        public string[]? Tags{get; set;}
        public DateTime CreatedAt{get; set;}
        public DateTime LastUpdated{get; set;}
        public string? AuthorName{get; set;}
        public string? AuthorID{get; set;}
        public int ViewCount{get; set;}
        public int LikeCount{get; set;}
        public int CommentCount{get; set;}
    }
}