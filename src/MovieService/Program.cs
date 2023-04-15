using MovieService.Common.Services;
using MovieService.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddRabbitMq(builder.Configuration);
    builder.Services.AddScoped<IMovieService, MovieServiceImp>();
};

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.Run();
};