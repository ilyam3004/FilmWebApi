namespace WatchlistService.Dtos.Responses;

public class WatchlistResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int MoviesCount { get; set; }
    public List<MovieResponse> Movies { get; set; }
    public DateTime DateTimeOfCreating { get; set; }
}