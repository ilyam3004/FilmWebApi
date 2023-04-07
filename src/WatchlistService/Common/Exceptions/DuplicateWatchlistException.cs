namespace WatchlistService.Common.Exceptions;

public class DuplicateWatchlistException : Exception
{
    public DuplicateWatchlistException(string message)
        : base(message) { }
}
