using TMDbLib.Objects.Movies;

namespace WatchlistService.Dtos.Responses;

public class WatchlistResponse
{
    public string Id { get; set; }
    public string Name { get; set; } 
    public List<Movie> Movies { get; set; }   
}