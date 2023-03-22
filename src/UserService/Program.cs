using UserService.Common.Authentication;
using Microsoft.EntityFrameworkCore;
using UserService.Common.Services;
using System.Reflection;
using FluentValidation;
using UserService.Data;

var builder = WebApplication.CreateBuilder(args);

{
    var assembly = Assembly.GetExecutingAssembly();
    Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));
    builder.Services.AddDbContext<AppDbContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

    builder.Services.Configure<JwtSettings>(
        builder.Configuration.GetSection(JwtSettings.SectionName)
    );

    builder.Services
        .AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>()
        .AddSingleton<IDateTimeProvider, DateTimeProvider>();

    builder.Services
        .AddScoped<IAccountService, AccountService>()
        .AddScoped<IUserRepository, UserRepository>();

    builder.Services
        .AddValidatorsFromAssembly(assembly)
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddControllers();
}

var app = builder.Build();

{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    DbInitializer.PrepeareDatabase(app);
    app.Run();
}
