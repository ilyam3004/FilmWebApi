using Microsoft.EntityFrameworkCore;
using UserService.Middleware;
using UserService.Services;
using UserService.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration
                .GetConnectionString("UserServiceDb")))
        .AddScoped<IUserService, UserService.Services.UserService>()
        .AddScoped<IUserRepository, UserRepository>()
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddControllers();
}

var app = builder.Build();
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}