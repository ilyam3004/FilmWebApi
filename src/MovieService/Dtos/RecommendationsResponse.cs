using TMDbLib.Objects.Search;

namespace MovieService.Dtos;

public class RecommendationsResponse
{
    public string WatchlistName { get; set; } = null!;
    public List<SearchMovie> Movies { get; set; } = null!;
}