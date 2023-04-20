namespace MovieService.Common.Exceptions;

public class MovieNotFoundException : Exception
{
    public MovieNotFoundException(
        string message = "No results by this search request") : base(message) { }    
}