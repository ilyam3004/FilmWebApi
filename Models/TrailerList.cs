using System.Collections.Generic;
using Newtonsoft.Json;

namespace FirstWebApi
{
    public class TrailerList
    {
        [JsonProperty("results")]
        public List<Trailer> Trailers { get; set; }
    }
}