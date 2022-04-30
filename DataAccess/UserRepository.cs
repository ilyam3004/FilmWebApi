using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using FirstWebApi.Authentification;

namespace FirstWebApi.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly IDynamoDBContext _dbContext;
        private readonly ITokenService _tokenService;

        public UserRepository(IDynamoDBContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public async Task<User> GetUser(string login)
            => await _dbContext.LoadAsync<User>(login.ToLower());
        
        public async Task<UserDto> AddUser(RegisterUser registerUser)
        {
            var hmac = new HMACSHA512();
            
            var user = new User
            {
                Login = registerUser.Login.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password)),
                PasswordSalt = hmac.Key
            };
            
            await _dbContext.SaveAsync(user);
            return new UserDto
            {
                Login = user.Login,
                Token = _tokenService.CreateToken(user)
            };
        }
        
        public bool UserExists(string login)
        {
            if (_dbContext.ScanAsync<User>(new ScanCondition[]
                {
                    new ScanCondition("Login", ScanOperator.Equal, login.ToLower())
                }).GetRemainingAsync().Result.Count == 0)
                return false;

            return true;
        }
    }
}