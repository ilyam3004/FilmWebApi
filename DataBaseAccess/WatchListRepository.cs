using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using System.Linq;
using FirstWebApi.TmdbAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FirstWebApi.DataBaseAccess
{
    public class WatchListRepository : IWatchListRepository
    {
        private readonly IDynamoDBContext _dbContext;
        private readonly ITMDBHttpClient _tmdbHttpClient;
        public WatchListRepository(IDynamoDBContext dbContext, ITMDBHttpClient tmdbHttpClient)
        {
            _tmdbHttpClient = tmdbHttpClient;
            _dbContext = dbContext;
        }
        public async Task<DBMovie> Add(DBMovie movie)
        {
            await _dbContext.SaveAsync(movie);
            return movie;
        }
        public JObject GetWatchList(string login)
        {
            List<DBMovie> allWatchLists = GetAllWatchlists();
            List<DBMovie> accountWatchilst = new();
            if (allWatchLists.Count != 0)
            {
                foreach (var item in allWatchLists)
                {
                    if (item.Login == login)
                        accountWatchilst.Add(item);
                }
            }
            else
                return null;

            MovieList movieList = new MovieList();
            movieList.Results = new List<Movie>();
            // ------------- testing ----------- 
            foreach (var item in accountWatchilst)
            {
                string tempResponse = _tmdbHttpClient.GetById(item.TmdbId);
                if(tempResponse != null)
                    movieList.Results.Add(JsonConvert.DeserializeObject<Movie>(tempResponse));
            }
            // ------------- testing ----------- 
            return JObject.Parse(JsonConvert.SerializeObject(movieList));
        }
        public async Task Delete(int id)
        {
            List<DBMovie> allWatchLists = GetAllWatchlists();
            List<DBMovie> accountWatchilst = new();
            if (allWatchLists.Count != 0)
            {
                foreach (var item in allWatchLists)
                {
                    if (item.TmdbId == id)
                        _dbContext.DeleteAsync(item);
                }
            }
        }
        public int GetLastId()
        {
            List<DBMovie> allWatchlists = GetAllWatchlists();
            if(allWatchlists.Count != 0)
                return allWatchlists.Max(x => x.id);
            return 0;
        }
        private List<DBMovie> GetAllWatchlists()
            => _dbContext.ScanAsync<DBMovie>(new ScanCondition[] {}).GetRemainingAsync().Result;
    }
}