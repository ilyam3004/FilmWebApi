namespace MovieService.Common.Exceptions;

public class MoviesNotFoundException : Exception
{
    public MoviesNotFoundException(
        string message = "No results by this search request") : base(message) { }    
}