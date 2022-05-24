using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FilmWebApi.Authentification;
using FilmWebApi.DataBaseAccess;
using FilmWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmWebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUser registerUser)
        {
            if (!_userRepository.UserExists(registerUser.Login))
                return await _userRepository.AddUser(registerUser);
            return BadRequest("User with the same login is already exists");
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>Login(LoginUser loginUser)
        {
            var user = await _userRepository.GetUser(loginUser.Login);
            if (user == null)
                return Unauthorized("Invalid Login");

            var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUser.Password));

            for(int i = 0; i < computedHash.Length;i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) 
                    return Unauthorized("Invalid Password");
            }
            return new UserDto()
            {
                Token = _tokenService.CreateToken(user),
                Login = user.Login
            };
        }
    }
}