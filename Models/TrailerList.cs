using System.Collections.Generic;
using Newtonsoft.Json;

namespace FilmWebApi
{
    public class TrailerList
    {
        [JsonProperty("results")]
        public List<Trailer> Trailers { get; set; }
    }
}