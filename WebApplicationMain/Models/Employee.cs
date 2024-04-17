using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplicationMain.Models
{
    [BsonIgnoreExtraElements]
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("age")]
        public int Age { get; set; }

        [BsonElement("role")]
        public string Role { get; set; } = string.Empty;
    }
}
