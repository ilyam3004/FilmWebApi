using System.Threading.Tasks;
using FirstWebApi.TmdbAccess;
using Microsoft.AspNetCore.Authorization;
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
        
        [Authorize]
        [HttpGet("gettopmovies")]
        public async Task<JObject> TopMovies()
            => _tmdbHttpClient.GetTopMovies();

        [Authorize]
        [HttpGet("search")]
        public async Task<JObject> Search(string title)
            => _tmdbHttpClient.SearchMovie(title);
    }
}