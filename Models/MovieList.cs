using Newtonsoft.Json;
using System.Collections.Generic;

namespace FilmWebApi
{
    public class MovieList
    {
        [JsonProperty("results")]
        public List<Movie> Results { get; set; }
    }
}