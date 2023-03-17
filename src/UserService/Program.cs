using Microsoft.EntityFrameworkCore;
using UserService.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration
                .GetConnectionString("UserServiceDb")))
        .AddScoped<IUserRepository, UserRepository>()
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