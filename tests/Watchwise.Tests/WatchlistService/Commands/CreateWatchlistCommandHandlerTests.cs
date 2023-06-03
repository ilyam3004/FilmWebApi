using AutoFixture;
using MassTransit.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WatchlistService.Bus;
using WatchlistService.Data.Repositories;
using WatchlistService.Messages.Commands.CreateWatchlist;

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
            _watchlistRequestClientMock.Object,);
    }

    public async Task Handler_ShouldReturnWatchlist() 
    {
        //Arrange
        
        //Act
        //Assert
    }
}
