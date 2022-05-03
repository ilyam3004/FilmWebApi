using Newtonsoft.Json;
using System.Collections.Generic;

namespace FirstWebApi
{
    public class MovieList
    {
        [JsonProperty("results")]
        public List<Movie> Results { get; set; }
    }
}