using Newtonsoft.Json;
using System.Collections.Generic;
using WeatherDB.Models;

namespace ApiDB.Models
{
    public class RootObject
    {
        public List<WeathersList> weathersList { get; set; }

        [JsonProperty("messageResponse")]
        public string messageResponse { get; set; }
    }
}
