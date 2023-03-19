using UserService.Models;

namespace UserService.Data;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddUser(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public bool UserExists(string login)
    {
        return _dbContext.Users.Any(u => u.Login == login);
    }
}