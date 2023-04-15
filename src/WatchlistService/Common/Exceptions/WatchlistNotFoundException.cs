namespace WatchlistService.Common.Exceptions;

public class WatchlistNotFoundException : Exception
{
    public WatchlistNotFoundException(
        string message = "Watchlist not found") 
        : base(message) { }
}