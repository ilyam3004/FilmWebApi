using UserService.Models;

namespace UserService.Data;

public interface IUserRepository
{
    bool SaveChanges();
    Task<User> GetUser(string login);
    Task AddUser(User user);
    bool UserExists(string login);
}