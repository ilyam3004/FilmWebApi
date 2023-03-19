using Microsoft.EntityFrameworkCore;
using UserService.Services;
using System.Reflection;
using FluentValidation;
using UserService.Data;

var builder = WebApplication.CreateBuilder(args);
{
    var assembly = Assembly.GetExecutingAssembly();
    
    builder.Services
        .AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration
                .GetConnectionString("UserServiceDb")))
        .AddScoped<IUserService, UserService.Services.UserService>()
        .AddScoped<IUserRepository, UserRepository>()
        .AddValidatorsFromAssembly(assembly)
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddControllers();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}