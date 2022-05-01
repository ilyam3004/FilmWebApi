using System.Collections.Generic;
using System.Threading.Tasks;
using FirstWebApi.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<Movie>> AddMovie(string login, string title)
            => await _watchListRepository.Add(new Movie
            {
                Login = login.ToLower(),
                Title = title,
                id = _watchListRepository.GetLastId() + 1
            });

        [Authorize]
        [HttpGet]
        public List<Movie> Watchlist(string login)
            => _watchListRepository.GetWatchList(login);
    }
}