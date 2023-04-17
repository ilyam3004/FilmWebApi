using MovieService.Common.Services;
using MovieService.Extensions;
using TMDbLib.Client;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddRabbitMq(builder.Configuration);
    builder.Services.AddScoped<IMovieService, MovieServiceImp>();
    builder.Services.AddSingleton<TMDbClient>(opt => 
        new TMDbClient(builder.Configuration["TmdbApiKey"]));
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
};

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Run();
};