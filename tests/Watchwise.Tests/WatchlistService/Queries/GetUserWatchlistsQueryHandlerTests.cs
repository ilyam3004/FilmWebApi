using AutoFixture;
using AutoMapper;
using Moq;
using TMDbLib.Objects.Movies;
using WatchlistService.Bus;
using WatchlistService.Bus.Clients;
using WatchlistService.Data.Repositories;
using WatchlistService.Messages.Queries.GetUserWatchlists;
using WatchlistService.Models;
using Watchwise.Tests.WatchlistService.Config;

namespace Watchwise.Tests.WatchlistService.Queries;

public class GetUserWatchlistsQueryHandlerTests
{
    private readonly GetUserWatchlistsQueryHandler _sut;
    private readonly Mock<IWatchListRepository> _watchlistRepositoryMock = new();
    private readonly Mock<IWatchlistRequestClient> _requestClientMock = new();
    private readonly Fixture _fixture;

    public GetUserWatchlistsQueryHandlerTests()
    {
        _fixture = new Fixture();
        IMapper mapper = AutoMapperInitializer.ConfigureAutoMapper();
        _sut = new GetUserWatchlistsQueryHandler(
            _watchlistRepositoryMock.Object,
            _requestClientMock.Object,
            mapper);
    }

    [Fact]
    public async Task Handler_ShouldReturnTheListOfWatchlistResponses()
    {
        //Arrange
        var query = _fixture.Create<GetUserWatchlistsQuery>();
        var watchlists = _fixture.CreateMany<Watchlist>(5)
            .ToList();

        var userId = Guid.NewGuid().ToString();
        
        _requestClientMock.Setup(x =>
            x.GetUserIdFromToken(query.Token)).ReturnsAsync(userId);

        _watchlistRepositoryMock.Setup(x =>
                x.GetWatchlistsAsync(userId))
            .ReturnsAsync(watchlists);

        _requestClientMock.Setup(x =>
                x.GetMoviesData(It.IsAny<List<int>>()))
            .ReturnsAsync(GenerateMovieObjects(watchlists.Count));

        //Act
        var response = await _sut.Handle(query, CancellationToken.None);

        //Assert
        Assert.Equal(response.Count, watchlists.Count);
    }
    
    private List<Movie> GenerateMovieObjects(int count)
    {
        var movies = Enumerable.Range(0, count).Select(_ => new Movie()).ToList();

        return movies;
    }
}