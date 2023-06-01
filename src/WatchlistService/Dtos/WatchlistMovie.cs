using MongoDB.Bson.Serialization.Attributes;

namespace WatchlistService.Dtos
{
    public class WatchlistMovie
    {
        [BsonElement("movie_id")]
        public int MovieId { get; set; }

        [BsonElement("date")]
        public DateTime DateTimeOfAdding { get; set; }
    }
}
