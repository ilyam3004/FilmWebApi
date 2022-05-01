using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.Xml;

namespace FirstWebApi.DataAccess
{
    public class WatchListRepository : IWatchListRepository
    {
        private readonly IDynamoDBContext _dbContext;

        public WatchListRepository(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _dbContext.SaveAsync(movie);
            return movie;
        }

        public List<Movie> GetWatchList(string login)
        {
            List<Movie> allWatchLists = _dbContext.ScanAsync<Movie>(new ScanCondition[] {}).GetRemainingAsync().Result;
            List<Movie> accountWatchilst = new();
            if (allWatchLists != null)
            {
                foreach (var item in allWatchLists)
                {
                    if (item.Login == login)
                        accountWatchilst.Add(item);
                }
            }
            else
                return null;
            return accountWatchilst;
        }

        public int GetLastId()
        {
            List<Movie> allWatchlist = _dbContext.ScanAsync<Movie>(new ScanCondition[] {}).GetRemainingAsync().Result;
            if(allWatchlist.Count != 0)
                return allWatchlist.Max(x => x.id);
            return 0;
        }
    }
}