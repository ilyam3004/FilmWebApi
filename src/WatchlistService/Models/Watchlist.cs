using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using WatchlistService.Dtos;

namespace WatchlistService.Models
{
    public class Watchlist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("user_id")]
        public string UserId { get; set; } = null!;
        
        [BsonElement("movies")]
        public List<WatchlistMovie> MoviesId { get; set; } = null!;

        [BsonElement("date")]
        public DateTime DateTimeOfCreating { get; set; }
    }
}