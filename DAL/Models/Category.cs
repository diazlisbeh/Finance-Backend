using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace Backend.DAL.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int value { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
    }
}


