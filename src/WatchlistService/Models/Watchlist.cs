using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WatchlistService.Models
{
    public class Watchlist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string WatchlistId { get; set; } = null!;

        [BsonElement("Name")]
        public string WatchlistName { get; set; } = null!;

        [BsonElement("UserId")]
        public string UserId { get; set; } = null!;
    }
}