using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherDB.Models;

namespace WeatherDB.Services
{
    public interface IWeatherService
    {
        List<WeathersList> GetWeathersCity(int cityCode);
        WeathersList Create(WeathersList apiTest);
        Task<List<WeathersList>> CreateMany(List<WeathersList> apiTest);
        void Update(string id, WeathersList apiTestIn);
        void Remove(WeathersList apiTestIn);
        void Remove(string id);
    }
}