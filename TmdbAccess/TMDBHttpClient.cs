using System;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace FirstWebApi.TmdbAccess
{
    public class TMDBHttpClient : ITMDBHttpClient
    {
        private readonly HttpClient _client;
        private const string API_KEY = "7f15e76697d018b192edceb6f098447b";

        public TMDBHttpClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        }
        [HttpGet]
        public JObject GetTopMovies()
            => JObject.Parse(_client
                .GetAsync($"movie/top_rated?api_key={API_KEY}&language=en-US&page=1")
                .Result
                .Content.ReadAsStringAsync().Result);
        [HttpGet]
        public JObject SearchMovie(string title)
            => JObject.Parse(_client
                .GetAsync($"search/movie?api_key={API_KEY}&query={title}")
                .Result.Content
                .ReadAsStringAsync()
                .Result);
        
    }
}