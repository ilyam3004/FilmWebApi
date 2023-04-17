namespace WatchlistService.Common.Exceptions;

public class MovieNotFoundException : Exception
{
    public MovieNotFoundException(
        string message = "Movie not found"): base(message) { }
}