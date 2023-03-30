using UserService.Models;

namespace UserService.Data;

public static class DbInitializer
{

    public static void PrepeareDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        Initialize(context!);
    }

    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();
        if (!context.Users.Any())
        {
            context.Users.Add(
                new User
                {
                    UserId = Guid.NewGuid(),
                    Login = "login@mail.com",
                    Password = Guid.NewGuid().ToString()
                }
            );

            context.Users.Add(
                new User
                {
                    UserId = Guid.NewGuid(),
                    Login = "login2@mail.com",
                    Password = Guid.NewGuid().ToString()
                }
            );

            context.Users.Add(
                new User
                {
                    UserId = Guid.NewGuid(),
                    Login = "login3@mail.com",
                    Password = Guid.NewGuid().ToString()
                }
            );
            context.SaveChanges();
        }
    }
}
