using Microsoft.Extensions.Options;
using WatchlistService.Models;
using MongoDB.Driver;

namespace WatchlistService.Data.DbContext
{
    public class WatchlistContext : IWatchlistContext
    {
        public WatchlistContext(
            IOptions<DatabaseSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);
            Watchlists = database.GetCollection<Watchlist>(
                options.Value.CollectionName);
        }
        public IMongoCollection<Watchlist> Watchlists { get; }
    }
}