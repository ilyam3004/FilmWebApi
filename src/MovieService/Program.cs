using MovieService.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddRabbitMq(builder.Configuration);
};

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.Run();
};