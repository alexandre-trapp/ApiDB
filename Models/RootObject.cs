using Newtonsoft.Json;
using System.Collections.Generic;
using WeatherDB.Models;

namespace ApiDB.Models
{
    public class RootObject
    {
        [JsonProperty("weathersList ")]
        public List<WeathersList> weathersList { get; set; }

        [JsonProperty("id ")]
        public string messageResponse { get; set; }
    }
}
