namespace MovieService.Common.Exceptions;

public class WatchlistsNotFoundException : Exception
{
    public WatchlistsNotFoundException(
        string message = "You don't have any watchlists."): base(message) { }
}