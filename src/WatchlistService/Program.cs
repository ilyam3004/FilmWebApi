using WatchlistService.Data.DbContext;
using WatchlistService.Data.Repositories;
using System.Reflection;
using FluentValidation;
using RabbitMQ.Client;
using WatchlistService.Common.Events;
using WatchlistService.Common.Services;

var builder = WebApplication.CreateBuilder(args);
{
    var assembly = Assembly.GetExecutingAssembly();

    builder.Services
        .AddScoped<IWatchlistService, WatchlistServiceImp>()
        .AddScoped<IWatchListRepository, WatchlistRepository>()
        .AddScoped<IWatchlistContext, WatchlistContext>()
        .AddSingleton<IMessageBusProducer, MessageBusProducer>()
        .AddValidatorsFromAssembly(assembly)
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddControllers();
    var factory = new ConnectionFactory
    {
        HostName = builder.Configuration["RabbitMQHost"],
    };
    
    builder.Services.AddSingleton(factory.CreateConnection());

    builder.Services.Configure<DatabaseSettings>(
        builder.Configuration.GetSection(DatabaseSettings.SectionName)
    );
}

var app = builder.Build();
{
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}