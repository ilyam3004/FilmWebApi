using System.Threading.Tasks;
using FirstWebApi.Authentification;

namespace FirstWebApi.DataAccess
{
    public interface IUserRepository
    {
        Task<User> GetUser(string login);
        Task<UserDto> AddUser(RegisterUser registerUser);
        bool UserExists(string login);
    }
}