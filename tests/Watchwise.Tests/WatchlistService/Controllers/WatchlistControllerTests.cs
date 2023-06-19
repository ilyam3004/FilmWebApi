using WatchlistService.Messages.Commands.RemoveMovie;
using Watchwise.Tests.WatchlistService.Config;
using WatchlistService.Controllers;
using AutoFixture;
using AutoMapper;
using MediatR;
using Moq;

namespace Watchwise.Tests.WatchlistService.Controllers;

public class WatchlistControllerTests
{
    private readonly WatchlistController _sut;
    private readonly Mock<ISender> _senderMock = new();
    private readonly Fixture _fixture;
    
    public WatchlistControllerTests()
    {
        _fixture = new Fixture();
        IMapper mapper = AutoMapperInitializer.ConfigureAutoMapper();
        _sut = new WatchlistController(_senderMock.Object, mapper);
    }

    public async Task Controller_ShouldRemoveMovieFromWatchlist()
    {
        //Arrange
        var command = _fixture.Create<RemoveMovieCommand>();

        //Act
        var response = await _sut.RemoveMovieFromWatchlist(
            It.IsAny<string>(), It.IsAny<int>());

        //Assert

    }
}