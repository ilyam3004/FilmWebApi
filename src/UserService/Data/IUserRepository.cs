using UserService.Models;

namespace UserService.Data;

public interface IUserRepository
{
    Task AddUser(User user);
    Task<User> GetUserByLogin(string login);
    bool UserExists(string login);
}