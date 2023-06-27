namespace Shared.Models;

public class SharedWatchlist
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public List<SharedWatchlistMovie> Movies { get; set; } = null!;
    public DateTime DateTimeOfCreating { get; set; }
}