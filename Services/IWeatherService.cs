using System.Collections.Generic;
using WeatherDB.Models;

namespace WeatherDB.Services
{
    public interface IWeatherService
    {
        List<WeathersList> GetWeathersCity(int cityCode);
        WeathersList Create(WeathersList apiTest);
        List<WeathersList> CreateMany(List<WeathersList> apiTest);
        List<Lista> CreateManyList(List<Lista> listaWeather);
        List<City> CreateManyCity(City city);
        Coordenates CreateManyCoordenates(Coordenates coords);
        Wind CreateManyWind(Wind wind);
        Sys CreateManySys(Sys sys); 
        Main CreateManyMain(Main main);
        List<Weather> CreateManyListWeather(List<Weather> weathers);
        Clouds CreateManyClouds(Clouds clouds);
        
        void Update(string id, WeathersList apiTestIn);
        void Remove(WeathersList apiTestIn);
        void Remove(string id);
    }
}