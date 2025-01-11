using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace BLOGAPI.Models{

    public class User{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id{get; set;} 
        public string FirstName{get; set;}
        public string LastName{get; set;}
        public string Email{get; set;}
        public string Password{get; set;}
        public string Role{get; set;}
        public DateTime DateCreated{get; set;}
        public bool Verified{get; set;}
    }
}