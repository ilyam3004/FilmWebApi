using System;
using System.Collections.Generic;
using System.Net.Http;
using FilmWebApi.Secret;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace FilmWebApi.TmdbAccess
{
    public class TMDBHttpClient : ITMDBHttpClient
    {
        private readonly HttpClient _client;

        public TMDBHttpClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        }
        
        [HttpGet]
        public JObject SearchMovie(string title)
            => MoviesJson(_client
                .GetAsync($"search/movie?api_key={Constants.API_KEY}&query={title}")
                .Result.Content.ReadAsStringAsync().Result);

        public Movie GetById(int id)
        {
            string response =  _client
                .GetAsync($"movie/{id}?api_key={Constants.API_KEY}")
                .Result.Content.ReadAsStringAsync().Result;
            Movie movie = JsonConvert.DeserializeObject<Movie>(response);
            movie.PosterPath = $"https://image.tmdb.org/t/p/original{movie.PosterPath}";
            movie.BackdropPath = $"https://image.tmdb.org/t/p/original{movie.BackdropPath}";
            return movie;
        }


        [HttpGet]
        public JObject GetPopularMovies()
            => MoviesJson(_client
                .GetAsync($"movie/top_rated?api_key={Constants.API_KEY}&language=en-US&page=1")
                .Result.Content.ReadAsStringAsync().Result);
        
        [HttpGet]
        public JObject GetDayTrending()
            => MoviesJson(_client
                .GetAsync($"trending/movie/day?api_key={Constants.API_KEY}")
                .Result.Content.ReadAsStringAsync().Result);

        [HttpGet]
        public JObject GetWeekTrending()
            => MoviesJson(_client
                .GetAsync($"trending/movie/week?api_key={Constants.API_KEY}")
                .Result.Content.ReadAsStringAsync().Result);


        [HttpGet]
        public JObject GetUpComing()
            => MoviesJson(_client
                .GetAsync($"movie/upcoming?api_key={Constants.API_KEY}&language=en-US&page=1")
                .Result.Content.ReadAsStringAsync().Result);

        private JObject MoviesJson(string response)
        {
            MovieList movieList = JsonConvert.DeserializeObject<MovieList>(response);
            foreach (var item in movieList.Results)
            {
                item.PosterPath = $"https://image.tmdb.org/t/p/original{item.PosterPath}";
                item.BackdropPath = $"https://image.tmdb.org/t/p/original{item.BackdropPath}";
                if (item.Video)
                    item.trailers = GetTrailers(item.Id);
            }
            string outputJson = JsonConvert.SerializeObject(movieList);
            return JObject.Parse(outputJson);
        }
        private List<Trailer> GetTrailers(int id)
        {
            string response = _client
                .GetAsync($"movie/{id}/videos?api_key={Constants.API_KEY}&language=en-US")
                .Result.Content.ReadAsStringAsync().Result;
            TrailerList trailerList = JsonConvert.DeserializeObject<TrailerList>(response);
            return trailerList.Trailers;
        }
    }
}