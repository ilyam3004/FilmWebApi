using System.Threading.Tasks;
using FirstWebApi.DataBaseAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FirstWebApi.Controllers
{
    [ApiController]
    [Route("api/watchlist")]
    public class WatchListController : ControllerBase
    {
        private readonly IWatchListRepository _watchListRepository;
        
        public WatchListController(IWatchListRepository watchListRepository)
        {
            _watchListRepository = watchListRepository;
        }

        [Authorize]
        [HttpPost("addmovie")]
        public async Task<ActionResult<DBMovie>> AddMovie(int id)
        {
            return await _watchListRepository.Add(new DBMovie
            {
                Login = User.Identity.Name,
                TmdbId = id,
                id = _watchListRepository.GetLastId() + 1
            });
        }

        [Authorize]
        [HttpGet]
        public JObject Watchlist()
            => _watchListRepository.GetWatchList(User.Identity.Name);

        [Authorize]
        [HttpDelete("delete")]
        public async Task DeleteMovie(int id)
            => _watchListRepository.Delete(id);
    }
}