using TMDbLib.Objects.Search;

namespace WatchlistService.Dtos.Responses;

public class RecommendationsResponse
{
    public string WatchlistName { get; set; } = null!;
    public List<SearchMovie> Movies { get; set; } = null!;
}