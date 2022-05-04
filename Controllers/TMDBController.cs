using FirstWebApi.TmdbAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FirstWebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TMDBController : Controller
    {
        private readonly ITMDBHttpClient _tmdbHttpClient;

        public TMDBController(ITMDBHttpClient tmdbHttpClient)
        {
            _tmdbHttpClient = tmdbHttpClient;
        }
        
        [HttpGet("top_rated")]
        public JObject TopMovies()
            => _tmdbHttpClient.GetPopularMovies();

        [HttpGet("search")]
        public JObject Search(string title)
            => _tmdbHttpClient.SearchMovie(title);

        [HttpGet("trending/day")]
        public JObject DayTrendingMovies()
            => _tmdbHttpClient.GetDayTrending();
        
        [HttpGet("trending/week")]
        public JObject WeekTrendingMovies()
            => _tmdbHttpClient.GetWeekTrending();
        
        [HttpGet("upcoming")]
        public JObject UpComing()
             => _tmdbHttpClient.GetUpComing();
        
        // [HttpGet("latest")]
        // public JObject Latest()
        //     => _tmdbHttpClient.GetLatest();
    }
}