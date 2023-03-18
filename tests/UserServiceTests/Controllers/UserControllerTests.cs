using UserService.Controllers;
using UserService.Data;
using AutoMapper;
using Moq;
using UserService.Dtos.Requests;

namespace UserServiceTests.Controllers;

public class UserControllerTests
{
    private readonly UserController _sut;
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();

    public UserControllerTests()
    {
        _sut = new UserController(_userRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Controller_ShouldReturnError_WhenPasswordsAreNotEqual()
    {
        var request = new RegisterRequest
        {
            Login = "ilyamelnichuk3004@gmail.com",
            Password = "pass",
            ConfirmPassword = "pass1"
        };


        await Assert.ThrowsAsync<InvalidDataException>
                (() => _sut.Register(request));
    }
}