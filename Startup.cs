using System;
using System.Text;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using FirstWebApi.Authentification;
using FirstWebApi.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FirstWebApi
{
    public class Startup
    {
        public IConfiguration _config { get; }
        
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddMvc();
            // services.AddControllers();
            //
            // //DynamoDB Access
            // var credentials = new BasicAWSCredentials("AKIAQ37NQIFXARODGHUO", "waWrgVcURHAf+CSzmv1xpBLjbj4ezVQkzk4Xhscr");
            // var config = new AmazonDynamoDBConfig()
            // {
            //     RegionEndpoint = RegionEndpoint.USEast1
            // };    
            // var client = new AmazonDynamoDBClient(credentials, config);
            //
            // //Singletons
            // services.AddSingleton<IAmazonDynamoDB>(client);
            // services.AddSingleton<IUserRepository, UserRepository>();
            // services.AddSingleton<ITokenService, TokenService>();
            // services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            //
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer(options =>
            //     {
            //         options.TokenValidationParameters = new()
            //         {
            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"])),
            //             ValidateIssuer = false,
            //             ValidateAudience = false
            //         };
            //     });
            //
            // //SwaggerConfiguration
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo
            //     {
            //         Title = "dynamodb_sample", 
            //         Version = "v1",
            //         Description = "TestApi"
            //     });
            // });
            // services.AddControllersWithViews();
            services.AddApplicationServices(_config);

            services.AddCors();
            services.AddIdentityServices(_config);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI( c=>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "dynamoDb_sample");
                c.RoutePrefix = String.Empty;
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
