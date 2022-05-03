using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using FirstWebApi.DataBaseAccess;
using FirstWebApi.TmdbAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


namespace FirstWebApi.Services
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddMvc();
            services.AddControllers();
            
            //DynamoDB Access
            var credentials = new BasicAWSCredentials("AKIAQ37NQIFXARODGHUO", "waWrgVcURHAf+CSzmv1xpBLjbj4ezVQkzk4Xhscr");
            var dynamoDbConfig = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.USEast1
            };    
            var client = new AmazonDynamoDBClient(credentials, dynamoDbConfig);
            
            //Singletons
            services.AddSingleton<IAmazonDynamoDB>(client);
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IWatchListRepository, WatchListRepository>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            //TMDBClient
            services.AddHttpClient<ITMDBHttpClient, TMDBHttpClient>();
            services.AddControllers().AddNewtonsoftJson();
            //SwaggerConfiguration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "dynamodb_sample", 
                    Version = "v1",
                    Description = "TestApi"
                });
            });
            return services;
        }
    }
}