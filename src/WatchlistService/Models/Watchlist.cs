using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WatchlistService.Models
{
    public class Watchlist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string WatchlistId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        [BsonElement("UserId")]
        public string UserId { get; set; } = null!;
    }
}