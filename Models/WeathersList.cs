using System;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace WeatherDB.Models
{
    [JsonObject("weathersList")]
    public class WeathersList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }

        [BsonElement("cod")]
        [JsonProperty("cod")]
        public int cod { get; set; }

        [BsonElement("message")]
        [JsonProperty("message")]
        public int message { get; set; }

        [BsonElement("cnt")]
        [JsonProperty("cnt")]
        public int cnt { get; set; }

        [BsonElement("list")]
        [JsonProperty("list")]
        public List<Lista> lista { get; set; }

        [BsonElement("city")]
        [JsonProperty("city")]
        public City city { get; set; }
    }

    [JsonObject("list")]
    public class Lista
    {
        [BsonElement("dt")]
        [JsonProperty("dt")]
        public int dt { get; set; }

        [BsonElement("wind")]
        [JsonProperty("wind")]
        public Wind wind { get; set; }

        [BsonElement("dt_txt")]
        [JsonProperty("dt_txt")]
        public string dt_txt { get; set; }

        [BsonElement("sys")]
        [JsonProperty("sys")]
        public Sys sys { get; set; }

        [BsonElement("main")]
        [JsonProperty("main")]
        public Main main { get; set; }

        [BsonElement("weather")]
        [JsonProperty("weather")]
        public List<Weather> weather { get; set; }

        [BsonElement("clouds")]
        [JsonProperty("clouds")]
        public Clouds clouds { get; set; }
    }

    [JsonObject("weather")]
    public class Weather
    {
        [BsonElement("id")]
        [JsonProperty("id")]
        public int id { get; set; }

        [BsonElement("main")]
        [JsonProperty("main")]
        public string main { get; set; }

        [BsonElement("description")]
        [JsonProperty("description")] 
        public string description { get; set; }

        [BsonElement("icon")]
        [JsonProperty("icon")]
        public string icon { get; set; }
    }

    [JsonObject("wind")]
    public class Wind
    {
        [BsonElement("speed")]
        [JsonProperty("speed")]
        public double speed { get; set; }
        
        [BsonElement("deg")]
        [JsonProperty("deg")]
        public int deg { get; set; }
    }

    [JsonObject("sys")]
    public class Sys
    {
        [BsonElement("pod ")]
        [JsonProperty("pod ")]
        public string pod { get; set; }
    }

    [JsonObject("main")]
    public class Main
    {
        [BsonElement("temp ")]
        [JsonProperty("temp ")]
        public double temp { get; set; }

        [BsonElement("feels_like ")]
        [JsonProperty("feels_like ")]
        public double feels_like { get; set; }

        [BsonElement("temp_min ")]
        [JsonProperty("temp_min ")]
        public double temp_min { get; set; }

        [BsonElement("temp_max ")]
        [JsonProperty("temp_max ")]
        public double temp_max { get; set; }

        [BsonElement("pressure ")]
        [JsonProperty("pressure ")]
        public int pressure { get; set; }

        [BsonElement("sea_level ")]
        [JsonProperty("sea_level ")]
        public int sea_level { get; set; }

        [BsonElement("grnd_level ")]
        [JsonProperty("grnd_level ")]
        public int grnd_level { get; set; }

        [BsonElement("humidity ")]
        [JsonProperty("humidity ")]
        public int humidity { get; set; }

        [BsonElement("temp_kf ")]
        [JsonProperty("temp_kf ")]
        public double temp_kf { get; set; }
    }

    [JsonObject("clouds")]
    public class Clouds
    {
        [BsonElement("all ")]
        [JsonProperty("all ")]
        public int all { get; set; }
    }

    [JsonObject("coord")]
    public class Coord
    {
        [BsonElement("lat ")]
        [JsonProperty("lat ")]
        public double lat { get; set; }

        [BsonElement("lon ")]
        [JsonProperty("lon ")]
        public double lon { get; set; }
    }

    [JsonObject("city")]
    public class City
    {
        [BsonElement("id ")]
        [JsonProperty("id ")]
        public int id { get; set; }

        [BsonElement("name ")]
        [JsonProperty("name ")]
        public string name { get; set; }

        [BsonElement("country ")]
        [JsonProperty("country ")]
        public string country { get; set; }

        [BsonElement("timezone ")]
        [JsonProperty("timezone ")]
        public int timezone { get; set; }

        [BsonElement("sunrise ")]
        [JsonProperty("sunrise ")]
        public int sunrise { get; set; }

        [BsonElement("sunset ")]
        [JsonProperty("sunset ")]
        public int sunset { get; set; }

        [BsonElement("Coord ")]
        [JsonProperty("Coord ")]
        public Coord Coord { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
