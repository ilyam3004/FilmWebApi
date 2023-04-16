namespace WatchlistService.Common.Exceptions;

public class DuplicateMovieInWatchlistException : Exception
{
    public DuplicateMovieInWatchlistException(
        string message = "This movie already exists in this watchlist"): base(message) {}
}