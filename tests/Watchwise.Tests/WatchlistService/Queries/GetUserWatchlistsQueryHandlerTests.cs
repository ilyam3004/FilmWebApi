using AutoFixture;
using AutoMapper;
using Moq;
using TMDbLib.Objects.Movies;
using WatchlistService.Bus;
using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Responses;
using WatchlistService.Messages.Queries.GetUserWatchlists;
using WatchlistService.Models;
using Watchwise.Tests.WatchlistService.Config;
using ZstdSharp.Unsafe;

namespace Watchwise.Tests.WatchlistService.Queries;

public class GetUserWatchlistsQueryHandlerTests
{
    private readonly GetUserWatchlistsQueryHandler _sut;
    private readonly Mock<IWatchListRepository> _watchlistRepositoryMock = new();
    private readonly Mock<IWatchlistRequestClient> _requestClientMock = new();
    private readonly Fixture _fixture;
    private readonly IMapper _mapper;

    public GetUserWatchlistsQueryHandlerTests()
    {
        _fixture = new Fixture();
        _mapper = AutoMapperInitializer.ConfigureAutoMapper();
        _sut = new GetUserWatchlistsQueryHandler(
            _watchlistRepositoryMock.Object,
            _requestClientMock.Object,
            _mapper);
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
        response.IfSucc(w =>
            Assert.Equal(w.Count, watchlists.Count));

        response.IfFail(w =>
            Assert.True(false, "Should not return an error"));
    }
    
    private List<Movie> GenerateMovieObjects(int count)
    {
        var movies = Enumerable.Range(0, count).Select(_ => new Movie()).ToList();

        return movies;
    }
}