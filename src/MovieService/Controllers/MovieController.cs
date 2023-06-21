using Microsoft.AspNetCore.Mvc;
using MovieService.Common.Services;

namespace MovieService.Controllers;

[ApiController]
[Route("movies")]
public class MovieController : ApiController
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet("search/{query}")]
    public async Task<IActionResult> SearchMovie(string query)
    {
        var result = await _movieService
            .SearchMovies(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet("{movieId}")]
    public async Task<IActionResult> GetMovie(int movieId)
    {
        var result = await _movieService.GetMovieData(movieId);

        return result.Match(Ok, Problem);
    }

    [HttpGet("popular")]
    public async Task<IActionResult> PopularMovies()
    {
        var result = await _movieService.GetPopularMovies();

        return result.Match(Ok, Problem);
    }

    [HttpGet("now-playing")]
    public async Task<IActionResult> NowPlayingMovies()
    {
        var result = await _movieService.GetNowPlayingMovies();

        return result.Match(Ok, Problem);
    }
    
    [HttpGet("top-rated")]
    public async Task<IActionResult> TopRatedMovies()
    {
        var result = await _movieService
            .GetTopRated();

        return result.Match(Ok, Problem);
    }
    
    [HttpGet("upcoming")]
    public async Task<IActionResult> Upcoming()
    {
        var result = await _movieService
            .GetUpcoming();

        return result.Match(Ok, Problem);
    }

    [HttpGet]
    public async Task<IActionResult> Recommendations()
    {
        string token = Request.Headers["Authorization"];
        var result = await _movieService.GetRecommendations(token);
        
        return result.Match(Ok, Problem);
    }
}
