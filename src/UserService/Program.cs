using UserService.Common.Authentication;
using Microsoft.EntityFrameworkCore;
using UserService.Common.Services;
using System.Reflection;
using FluentValidation;
using UserService.Common.Events;
using UserService.Data;
using UserService.Extensions;

var builder = WebApplication.CreateBuilder(args);

{
    var assembly = Assembly.GetExecutingAssembly();

    builder.Services.AddDbContext<AppDbContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

    builder.Services.Configure<JwtSettings>(
        builder.Configuration.GetSection(JwtSettings.SectionName)
    );

    builder.Services
        .AddSingleton<IJwtTokenService, JwtTokenService>()
        .AddSingleton<IDateTimeProvider, DateTimeProvider>()
        .AddHostedService<MessageBusSubscriber>();

    builder.Services
        .AddScoped<IUserService, UserServiceImp>()
        .AddScoped<IUserRepository, UserRepository>();

    builder.Services.AddRabbitMq(builder.Configuration);
    
    builder.Services
        .AddValidatorsFromAssembly(assembly)
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddControllers();
}

var app = builder.Build();
{
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}