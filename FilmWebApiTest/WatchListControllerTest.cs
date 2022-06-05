using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FilmWebApi;
using Microsoft.Extensions.Localization.Internal;
using Newtonsoft.Json;
using Xunit;

namespace FilmWebApiTest
{
    public class WatchListControllerTest
    {
        private HttpClient _httpClient;
        public WatchListControllerTest()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://movie-web-api-service.herokuapp.com/api/watchlist/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdEBnbWFpbC5jb20iLCJuYmYiOjE2NTQ0NTY4MDcsImV4cCI6MTY4NTk5MjgwN30.z8a__rzVuGpmMBZV1vZmDIFEYFGj71IKKIPc_l1-Hew");
        }
        
        [Fact]
        public async Task AddMovie_Test()
        {
            //Arrange
            var data = new StringContent("", Encoding.UTF8, "application/json");
            //Act
            var response = await _httpClient.PostAsync("addmovie?id=671", data);
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<DBMovie>(JsonConvert.DeserializeObject<DBMovie>(result));
        }

        [Fact]
        public async Task EmptyWatchList_Test()
        {
            //Act
            var response = await _httpClient.GetAsync("");
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<MovieList>(JsonConvert.DeserializeObject<MovieList>(result));
            Assert.NotEmpty(JsonConvert.DeserializeObject<MovieList>(result)?.Results);
        }
        
        [Fact]
        public async Task Watchlist_Empty_Test()
        {
            //Act
            var response = await _httpClient.GetAsync("");
            var result = await response.Content.ReadAsStringAsync();
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<MovieList>(JsonConvert.DeserializeObject<MovieList>(result));
            Assert.Empty(JsonConvert.DeserializeObject<MovieList>(result)?.Results);
        }
        
        [Fact]
        public async Task RemoveMovie_Test()
        {
            //Act
            var response = await _httpClient.DeleteAsync("delete?id=671");
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}