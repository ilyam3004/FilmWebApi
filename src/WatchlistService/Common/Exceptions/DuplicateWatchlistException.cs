namespace WatchlistService.Common.Exceptions;

public class DuplicateWatchlistException : Exception
{
    public DuplicateWatchlistException(
        string message = "Watchlist with this name already exists")
        : base(message) { }
}
