using UserService.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Data;
using AutoMapper;
using UserService.Services;

namespace UserService.Controllers;

[Route("users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public UserController(
        IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = _userService.Register(request);

        return Ok(request);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = _userService.Login(request);

        return Ok(request);
    }
}
