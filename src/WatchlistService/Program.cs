using WatchlistService.Data.Repositories;
using WatchlistService.Data.DbContext;
using WatchlistService.Extensions;
using WatchlistService.Bus;
using System.Reflection;
using FluentValidation;
using WatchlistService.Bus.Clients;

var builder = WebApplication.CreateBuilder(args);
{
    var assembly = Assembly.GetExecutingAssembly();

    builder.Services
        .AddScoped<IWatchListRepository, WatchlistRepository>()
        .AddScoped<IWatchlistContext, WatchlistContext>()
        .AddScoped<IWatchlistRequestClient, WatchlistRequestClient>()
        .AddValidatorsFromAssembly(assembly)
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddSwaggerGen()
        .AddControllers();

    builder.Services.AddAuth(builder.Configuration);

    builder.Services.AddRabbitMq(builder.Configuration);

    builder.Services.AddMediatR(c =>
        c.RegisterServicesFromAssemblyContaining<Program>());
    
    builder.Services.Configure<DatabaseSettings>(
        builder.Configuration.GetSection(DatabaseSettings.SectionName)
    );
}

var app = builder.Build();
{
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Run();
}
