using UserService.Models;

namespace UserService.Data;

public interface IUserRepository
{
    Task AddUser(User user);
    bool UserExists(string login);
}
