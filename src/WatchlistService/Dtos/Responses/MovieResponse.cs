using TMDbLib.Objects.Changes;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;

namespace WatchlistService.Dtos
{
    public class MovieResponse
    {
        public Movie Movie { get; set; }
        public DateTime DateTimeOfAdding { get; set; }
    }
}