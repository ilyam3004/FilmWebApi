using System.Threading.Tasks;
using FilmWebApi.Authentification;

namespace FilmWebApi.DataBaseAccess
{
    public interface IUserRepository
    {
        Task<User> GetUser(string login);
        Task<UserDto> AddUser(RegisterUser registerUser);
        bool UserExists(string login);
    }
}