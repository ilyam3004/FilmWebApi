namespace Shared.Models;

public class Watchlist
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public List<WatchlistMovie> Movies { get; set; } = null!;
    public DateTime DateTimeOfCreating { get; set; }
}