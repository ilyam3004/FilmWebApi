using UserService.Models;

namespace UserService.Data;

public interface IUserRepository
{
    bool SaveChanges();
    Task<User> GetUser(string login);
    void AddUser(User user);
    bool UserExists(string login);
}