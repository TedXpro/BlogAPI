using MongoDB.Bson; 
using MongoDB.Bson.Serialization.Attributes; 
 
namespace BLOGAPI.Models{ 
    public class Like{ 
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)] 
        public string? Id{get; set;} 
        public string? BlogId{get; set;} 
        public string? UserId{get; set;} 
        public string? Type{get; set;} 
    }
}