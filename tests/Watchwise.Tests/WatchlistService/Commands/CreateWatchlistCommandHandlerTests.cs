using AutoFixture;
using AutoMapper;
using Moq;
using WatchlistService.Bus.Clients;
using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Messages.Commands.CreateWatchlist;
using WatchlistService.Models;
using Watchwise.Tests.WatchlistService.Config;

namespace Watchwise.Tests.WatchlistService.Commands;

public class CreateWatchlistCommandHandlerTests
{
    private readonly Mock<IWatchListRepository> _watchlistRepositoryMock = new();
    private readonly Mock<IWatchlistRequestClient> _watchlistRequestClientMock = new();
    private readonly CreateWatchlistCommandHandler _sut;
    private readonly Fixture _fixture;

    public CreateWatchlistCommandHandlerTests()
    {
        _fixture = new Fixture();
        _sut = new CreateWatchlistCommandHandler(
            _watchlistRepositoryMock.Object,
            _watchlistRequestClientMock.Object);
    }

    [Fact]
    public async Task Handler_ShouldReturnWatchlist()
    {
        // Arrange
        var command = _fixture.Create<CreateWatchlistCommand>();
        var userId = Guid.NewGuid().ToString();

        _watchlistRequestClientMock.Setup(x => x
            .GetUserIdFromToken(command.Token)).ReturnsAsync(userId);

        _watchlistRepositoryMock.Setup(x =>
                x.WatchlistExistsByNameAsync(userId, command.WatchlistName))
            .ReturnsAsync(false);

        // Act
        var response = await _sut
            .Handle(command, CancellationToken.None);
        
        //Assert
        response.IfSucc(w =>
        {
            Assert.Equal(w.Movies, new List<WatchlistMovie>());
            Assert.Equal(w.Name, command.WatchlistName);
            Assert.Equal(w.UserId, userId);
        });
        
        response.IfFail(e =>
        {
            Assert.True(false, "Should not return error");
        });
    }
    
    [Fact]
    public async Task Handler_ShouldReturnDuplicateWatchlistException_WhenWatchlistExists()
    {
        // Arrange
        var command = _fixture.Create<CreateWatchlistCommand>();
        var userId = Guid.NewGuid().ToString();

        _watchlistRequestClientMock.Setup(x => x
            .GetUserIdFromToken(command.Token)).ReturnsAsync(userId);

        _watchlistRepositoryMock.Setup(x =>
                x.WatchlistExistsByNameAsync(userId, command.WatchlistName))
            .ReturnsAsync(true);

        // Act
        var response = await _sut
            .Handle(command, CancellationToken.None);
        
        //Assert
        response.IfSucc(w =>
        {
            Assert.True(false, "Should not return watchlist");
        });
        
        response.IfFail(e =>
        {
            Assert.IsType<DuplicateWatchlistException>(e);
        });
    }
}