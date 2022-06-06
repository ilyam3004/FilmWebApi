using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FilmWebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace FilmWebApiTest
{
    public class TMDBControllerTest
    {
        private HttpClient _client;
        public TMDBControllerTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            _client = factory.CreateClient();
        }
        
        [Fact]
        public async Task GetTopRated_Test()
        {
            //Act
            var response = _client.GetAsync("/api/top_rated").Result;
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<MovieList>(JsonConvert.DeserializeObject<MovieList>(result));
            Assert.NotEmpty(JsonConvert.DeserializeObject<MovieList>(result)!.Results);
        }

        [Fact]
        public async Task Search_Test()
        {
            //Act
            var response = _client.GetAsync("/api/search?title=harry").Result;
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<MovieList>(JsonConvert.DeserializeObject<MovieList>(result));
            Assert.NotEmpty(JsonConvert.DeserializeObject<MovieList>(result)!.Results);
        }

        [Fact]
        public async Task Search_NonExistent_Movie_Test()
        {
            //Act
            var response = _client.GetAsync("/api/search?title=asdkjasdj").Result;
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<MovieList>(JsonConvert.DeserializeObject<MovieList>(result));
            Assert.Empty(JsonConvert.DeserializeObject<MovieList>(result)!.Results);
        }
        
        [Fact]
        public async Task GetDayTrending_Test()
        {
            //Act
            var response = _client.GetAsync("/api/trending/day").Result;
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<MovieList>(JsonConvert.DeserializeObject<MovieList>(result));
            Assert.NotEmpty(JsonConvert.DeserializeObject<MovieList>(result)!.Results);
        }
        
        [Fact]
        public async Task GetWeekTrending_Test()
        {
            //Act
            var response = _client.GetAsync("/api/trending/week").Result;
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<MovieList>(JsonConvert.DeserializeObject<MovieList>(result));
            Assert.NotEmpty(JsonConvert.DeserializeObject<MovieList>(result)!.Results);
        }
        
        [Fact]
        public async Task GetById_Test()
        {
            //Act
            var response = _client.GetAsync("/api/top_rated").Result;
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<Movie>(JsonConvert.DeserializeObject<Movie>(result));
        }
        
        [Fact]
        public async Task GetPopular_Test()
        {
            //Act
            var response = _client.GetAsync("/api/popular").Result;
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<MovieList>(JsonConvert.DeserializeObject<MovieList>(result));
            Assert.NotEmpty(JsonConvert.DeserializeObject<MovieList>(result)!.Results);
        }
        
        [Fact]
        public async Task GetUpcoming_Test()
        {
            //Act
            var response = _client.GetAsync("/api/upcoming").Result;
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<MovieList>(JsonConvert.DeserializeObject<MovieList>(result));
            Assert.NotEmpty(JsonConvert.DeserializeObject<MovieList>(result)!.Results);
        }
     }
}
