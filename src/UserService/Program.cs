using UserService.Common.Authentication;
using Microsoft.EntityFrameworkCore;
using UserService.Common.Services;
using System.Reflection;
using FluentValidation;
using UserService.AsyncDataServices;
using UserService.Data;
using UserService.EventProcessing;

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
        .AddSingleton<IEventProcessor, EventProcessor>()
        .AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>()
        .AddSingleton<IDateTimeProvider, DateTimeProvider>()
        .AddHostedService<MessageBusSubscriber>();
    
    builder.Services
        .AddScoped<IUserService, UserServiceImp>()
        .AddScoped<IUserRepository, UserRepository>();

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