using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Data;
using UserService.Dtos;
using AutoMapper;

namespace UserService.Controllers;

[Route("users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserController(
        IUserRepository userRepository, 
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto request)
    {
        if (request.Password != request.ConfirmPassword)
        {
            return BadRequest("Passwords do not match");
        }

        await _userRepository.AddUser(_mapper.Map<User>(request));

        return Ok(request);
    }
}
