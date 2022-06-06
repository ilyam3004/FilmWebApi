using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using FilmWebApi;
using FilmWebApi.Authentification;
using Newtonsoft.Json;
using Xunit;

namespace FilmWebApiTest
{
    public class UserControllerTest
    {
        private HttpClient _httpClient;
        private AmazonDynamoDBClient _dbClient;
        
        public UserControllerTest()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://movie-web-api-service.herokuapp.com/api/");
            
            var credentials = new BasicAWSCredentials(Constants.ACCESS_KEY, Constants.SECRET_KEY);
            var dynamoDbConfig = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.USEast1
            };    
            _dbClient = new AmazonDynamoDBClient(credentials, dynamoDbConfig);
        }

        [Fact]
        public async Task Registration_Test()
        {
            //Arrange
            var user = new RegisterUser
            {
                Login = "test@email.com",
                Password = "testPass"
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            
            //Act
            var response = await _httpClient.PostAsync("register", data);
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<UserDto>(JsonConvert.DeserializeObject<UserDto>(result));
            
            UserDto userDto = JsonConvert.DeserializeObject<UserDto>(result);
            
            Assert.NotNull(userDto.Login);
            Assert.NotNull(userDto.Token);
            
            //Deleting test user from db
            var request = new DeleteItemRequest {
                TableName = "User",
                Key = new Dictionary <string, AttributeValue> {
                    {
                        "Login", new AttributeValue("test@email.com")
                    }
                },
            };
            
            await _dbClient.DeleteItemAsync(request);
        }

        [Fact]
        public async Task Register_An_Existing_User_Test()
        {
            //Arrange
            var user = new RegisterUser
            {
                Login = "test@gmail.com",
                Password = "testPass"
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            
            //Act
            var response = await _httpClient.PostAsync("register", data);
            var result = await response.Content.ReadAsStringAsync();
            
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Login_Test()
        {
            //Arrange
            var user = new LoginUser()
            {
                Login = "test@gmail.com",
                Password = "testPass"
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            
            //Act
            var response = await _httpClient.PostAsync("login", data);
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<UserDto>(JsonConvert.DeserializeObject<UserDto>(result));
            
            UserDto userDto = JsonConvert.DeserializeObject<UserDto>(result);
            
            Assert.NotNull(userDto.Login);
            Assert.NotNull(userDto.Token);
        }
        
        [Fact]
        public async Task Login_Not_Existing_User_Test()
        {
            //Arrange
            var user = new LoginUser()
            {
                Login = "as,daskdljaslkdlaskjd@gmail.com",
                Password = "testPass"
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            
            //Act
            var response = await _httpClient.PostAsync("login", data);
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
