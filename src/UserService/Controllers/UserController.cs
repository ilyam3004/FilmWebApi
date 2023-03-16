using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Data;

namespace UserService.Controllers;

[Route("users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto user)
    {
        if (user.Password != user.ConfirmPassword)
        {
            return BadRequest("Passwords do not match");
        }

        _userRepository.AddUser(new User
        {
            UserId = Guid.NewGuid().ToString(),
            Login = user.Login,
            Password = user.Password
        });

        return Ok(user);
    }
}
