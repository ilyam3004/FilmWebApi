using WatchlistService.Data.Repositories;
using WatchlistService.Common.Services;
using WatchlistService.Data.DbContext;
using WatchlistService.Common.Events;
using WatchlistService.Extensions;
using System.Reflection;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
{
    var assembly = Assembly.GetExecutingAssembly();

    builder.Services
        .AddScoped<IWatchlistService, WatchlistServiceImp>()
        .AddScoped<IWatchListRepository, WatchlistRepository>()
        .AddScoped<IWatchlistContext, WatchlistContext>()
        .AddValidatorsFromAssembly(assembly)
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddControllers();

    builder.Services.AddAuth(builder.Configuration);

    builder.Services.AddRabbitMq(builder.Configuration);

    builder.Services.Configure<DatabaseSettings>(
        builder.Configuration.GetSection(DatabaseSettings.SectionName)
    );
}

var app = builder.Build();
{
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
