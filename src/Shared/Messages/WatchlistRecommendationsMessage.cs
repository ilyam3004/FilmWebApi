namespace Shared.Messages;

public class WatchlistRecommendationsMessage
{
    public List<int> MoviesId { get; set; }
    public int MoviesCount { get; set; }
}