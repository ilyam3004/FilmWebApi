using System.Linq.Expressions;
using MovieService.Common.Exceptions;
using MovieService.Bus.Clients;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using LanguageExt.Common;
using MovieService.Dtos;
using Shared.Models;
using TMDbLib.Client;

namespace MovieService.Common.Services;

public class MovieServiceImp : IMovieService
{
    private readonly IMovieRequestClient _movieRequestClient;
    private readonly TMDbClient _movieClient;
    private const int MaxCountOfMoviesInWatchlist = 20;
    private const int PagesCount = 1;

    public MovieServiceImp(TMDbClient movieClient,
        IMovieRequestClient movieRequestClient)
    {
        _movieClient = movieClient;
        _movieRequestClient = movieRequestClient;
    }

    public async Task<Result<Movie>> GetMovieData(int movieId)
    {
        Movie movie = await _movieClient.GetMovieAsync(movieId);

        if (movie is null)
        {
            var exception = new MovieNotFoundException("Movie not found");
            return new Result<Movie>(exception);
        }

        movie = await GetMovieVideos(movie);
        movie = await GetRecommendations(movie);
        movie = await GetSimilarMovies(movie);

        GetMovieImagesLinks(ref movie);

        return movie;
    }

    public async Task<List<Movie>> GetMoviesData(List<int> moviesId)
    {
        var movies = new List<Movie>();

        foreach (var movieId in moviesId)
        {
            var movie = await _movieClient.GetMovieAsync(movieId);
            if (movie is not null)
            {
                movies.Add(movie);
            }
        }

        GetMovieImagesLinks(ref movies);

        return movies;
    }

    public async Task<Result<List<SearchMovie>>> SearchMovies(string query)
    {
        var movies = await _movieClient
            .SearchMovieAsync(query, PagesCount);

        if (movies.Results.Count == 0)
        {
            var exception = new MovieNotFoundException();
            return new Result<List<SearchMovie>>(exception);
        }

        GetMovieImagesLinks(ref movies);

        List<SearchMovie> sortedMovies = movies.Results
            .OrderByDescending(m => m.ReleaseDate).ToList();

        return sortedMovies;
    }

    public async Task<Result<List<SearchMovie>>> GetPopularMovies()
    {
        var movies = await _movieClient
            .GetMoviePopularListAsync(page: PagesCount);

        if (movies is null)
        {
            var exception = new MovieNotFoundException();
            return new Result<List<SearchMovie>>(exception);
        }

        GetMovieImagesLinks(ref movies);

        return movies.Results.ToList();
    }

    public async Task<Result<List<SearchMovie>>> GetNowPlayingMovies()
    {
        var movies = await _movieClient.GetMovieNowPlayingListAsync(page: PagesCount);

        if (movies is null)
        {
            var exception = new MovieNotFoundException();
            return new Result<List<SearchMovie>>(exception);
        }

        GetMovieImagesLinks(ref movies);

        return movies.Results.ToList();
    }

    public async Task<Result<List<SearchMovie>>> GetTopRated()
    {
        var movies = await _movieClient
            .GetMovieTopRatedListAsync(page: PagesCount);

        if (movies is null)
        {
            var exception = new MovieNotFoundException();
            return new Result<List<SearchMovie>>(exception);
        }

        GetMovieImagesLinks(ref movies);

        return movies.Results.ToList();
    }

    public async Task<Result<List<SearchMovie>>> GetUpcoming()
    {
        var movies = await _movieClient
            .GetMovieUpcomingListAsync(page: PagesCount);

        if (movies is null)
        {
            var exception = new MovieNotFoundException();
            return new Result<List<SearchMovie>>(exception);
        }

        GetMovieImagesLinks(ref movies);

        return movies.Results.ToList();
    }

    public async Task<Result<List<RecommendationsResponse>>> GetRecommendations(string token)
    {
        var userId = await _movieRequestClient.GetUserIdFromToken(token);

        var watchlists = await _movieRequestClient.GetWatchlists(userId);
        
        if (watchlists.Count == 0)
        {
            var exception = new WatchlistsNotFoundException();
            return new Result<List<RecommendationsResponse>>(exception);
        }
        
        var recommendationsResponse = new List<RecommendationsResponse>();
        foreach (var watchlist in watchlists)
        {
            int countOfMovies = MaxCountOfMoviesInWatchlist / watchlist.Movies.Count;
            var recommendations = new List<SearchMovie>();
            
            foreach (var movie in watchlist.Movies)
            {
                var movieRecommendations = await _movieClient
                    .GetMovieRecommendationsAsync(movie.MovieId, PagesCount);
                
                recommendations.AddRange(
                    movieRecommendations.Results.Take(countOfMovies).ToList());
            }
            
            recommendationsResponse.Add(
                new RecommendationsResponse
                {
                    WatchlistName = watchlist.Name,
                    Movies = recommendations
                });
        }

        return recommendationsResponse;
    }

    private async Task<> GetMovieRecommendations(List<Watchlist watchlists) 
    {
        
    }

    private async Task<Movie> GetRecommendations(Movie movie)
    {
        var recommendations = await _movieClient
            .GetMovieRecommendationsAsync(movie.Id, PagesCount);

        GetMovieImagesLinks(ref recommendations);
        movie.Recommendations = recommendations;

        return movie;
    }

    private async Task<Movie> GetSimilarMovies(Movie movie)
    {
        var similarMovies = await _movieClient
            .GetMovieSimilarAsync(movie.Id, PagesCount);

        GetMovieImagesLinks(ref similarMovies);
        movie.Similar = similarMovies;

        return movie;
    }

    private async Task<Movie> GetMovieVideos(Movie movie)
    {
        var videos = await _movieClient
            .GetMovieVideosAsync(movie.Id);

        foreach (var video in videos.Results)
        {
            if (video.Site != "YouTube")
            {
                videos.Results.Remove(video);
            }
        }

        movie.Videos = videos;
        movie.Video = videos.Results.Count != 0;

        return movie;
    }

    private void GetMovieImagesLinks(
        ref SearchContainer<SearchMovie> movies)
    {
        movies.Results.ForEach(m => GetMovieImagesLinks(ref m));
    }

    private void GetMovieImagesLinks(
        ref List<Movie> movies)
    {
        movies.ForEach(m => GetMovieImagesLinks(ref m));
    }

    private void GetMovieImagesLinks(
        ref SearchContainerWithDates<SearchMovie> movies)
    {
        movies.Results.ForEach(m => GetMovieImagesLinks(ref m));
    }

    private void GetMovieImagesLinks(
        ref Movie movie)
    {
        movie.BackdropPath = "https://image.tmdb.org/t/p/original" + movie.BackdropPath;
        movie.PosterPath = "https://image.tmdb.org/t/p/original" + movie.PosterPath;
        movie.ProductionCompanies.ForEach(c => c.LogoPath =
            "https://image.tmdb.org/t/p/original" + c.LogoPath);
    }

    private void GetMovieImagesLinks(
        ref SearchMovie movie)
    {
        movie.BackdropPath = "https://image.tmdb.org/t/p/original" + movie.BackdropPath;
        movie.PosterPath = "https://image.tmdb.org/t/p/original" + movie.PosterPath;
    }
}