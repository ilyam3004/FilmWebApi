using Newtonsoft.Json;

namespace FirstWebApi
{
    public class Trailer
    {
        [JsonProperty("iso_639_1")]
        public string Iso6391 { get; set; }

        [JsonProperty("iso_3166_1")]
        public string Iso31661 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("site")]
        public string Site { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}