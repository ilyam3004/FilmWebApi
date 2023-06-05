using AutoMapper;
using UserService.Profiles;
using WatchlistService.Common.Profiles;

namespace Watchwise.Tests.WatchlistService.Config;

public class AutoMapperInitializer
{
    public static IMapper ConfigureAutoMapper()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WatchlistProfile>();
            cfg.AddProfile<UserProfile>();
        });

        return mapperConfiguration.CreateMapper();
    }
}

[CollectionDefinition("AutoMapperCollection")]
public class AutoMapperCollection : ICollectionFixture<AutoMapperInitializer>
{
}