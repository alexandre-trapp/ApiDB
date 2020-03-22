using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;
using WeatherDB.Models;
using System.Threading.Tasks;
using System;

namespace WeatherDB.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IMongoCollection<WeathersList> WeathersColl;
        private readonly IMongoCollection<Lista> ListaColl;

        public WeatherService(IWeatherDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            WeathersColl = database.GetCollection<WeathersList>(settings.WeatherCollectionName);
            ListaColl = database.GetCollection<Lista>("Lista");
        }

        public List<WeathersList> Get() =>
            WeathersColl.Find(x => true).ToList();

        public List<WeathersList> GetWeathersCity(int cityCode) =>
            WeathersColl.Find<WeathersList>(apiTest => apiTest.city.id == cityCode).ToList();

        public WeathersList Create(WeathersList apiTest)
        {
            WeathersColl.InsertOne(apiTest);
            return apiTest;
        }

        public void Update(string id, WeathersList apiTestIn) =>
            WeathersColl.ReplaceOne(apiTest => apiTest._Id == id, apiTestIn);

        public void Remove(WeathersList apiTestIn) =>
            WeathersColl.DeleteOne(apiTest => apiTest._Id == apiTestIn._Id);

        public void Remove(string id) =>
            WeathersColl.DeleteOne(apiTest => apiTest._Id == id);

        public List<WeathersList> CreateMany(List<WeathersList> apiTest)
        {
            var apiRetorno = InsertRecords(apiTest);
            return apiRetorno;
        }

        private List<WeathersList> InsertRecords(List<WeathersList> apiTest)
        {
            WeathersColl.InsertMany(apiTest);

            foreach (var api in apiTest)
            {
                CreateManyList(api.lista);
            }

            return apiTest;
        }

        public List<Lista> CreateManyList(List<Lista> listaWeather)
        {
            ListaColl.InsertMany(listaWeather);
            return listaWeather;
        }

        List<City> IWeatherService.CreateManyCity(City city)
        {
            throw new NotImplementedException();
        }

        Coordenates IWeatherService.CreateManyCoordenates(Coordenates coords)
        {
            throw new NotImplementedException();
        }

        Wind IWeatherService.CreateManyWind(Wind wind)
        {
            throw new NotImplementedException();
        }

        Sys IWeatherService.CreateManySys(Sys sys)
        {
            throw new NotImplementedException();
        }

        Main IWeatherService.CreateManyMain(Main main)
        {
            throw new NotImplementedException();
        }

        List<Weather> IWeatherService.CreateManyListWeather(List<Weather> weathers)
        {
            throw new NotImplementedException();
        }

        Clouds IWeatherService.CreateManyClouds(Clouds clouds)
        {
            throw new NotImplementedException();
        }
    }
}