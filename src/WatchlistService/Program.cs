using System.Reflection;
using FluentValidation;
using WatchlistService.Services;

var builder = WebApplication.CreateBuilder(args);

{
    var assembly = Assembly.GetExecutingAssembly();

    builder.Services
        .AddScoped<WatchlistsService>()
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
