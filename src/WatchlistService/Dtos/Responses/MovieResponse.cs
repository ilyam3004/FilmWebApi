using TMDbLib.Objects.Movies;

namespace WatchlistService.Dtos
{
    public class MovieResponse
    {
        public Movie Movie { get; set; }
        public DateTime DateTimeOfAdding { get; set; }
    }
}