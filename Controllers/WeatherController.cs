using System;
using System.Linq;
using System.Threading.Tasks;
using ApiDB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WeatherDB.Services;

namespace WeatherDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _apiService;

        public WeatherController(WeatherService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            try
            {
                var retorno = _apiService.Get();
                return Ok(Utf8Json.JsonSerializer.ToJsonString(retorno));
            }
            catch (TimeoutException e)
            {
                return NotFound($"Não foi possível conectar ao servidor do banco de dados MongoDb, verifique se o serviço está ativo e estável no servidor. - Erro: {e.Message}");
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        } 

        [HttpGet("{cityCode:length(24)}")]
        public IActionResult Get([FromRoute] int cityCode)
        {
            var api = _apiService.GetWeathersCity(cityCode);

            if (api == null)
            {
                return NotFound();
            }

            return Ok(Utf8Json.JsonSerializer.ToJsonString(api));
        }

        [HttpPost]
        [Route("search/{city}")]
        public async Task<IActionResult> SerchWeatherCityApi([FromRoute] string city)
        {
            var _apiWeather = new ConnectWeatherApiService(new RestClient());
            var response = await _apiWeather.ConsumeWeatherApiService(new string[1] { city });

            return await ProcessWeathersList(response);
        }

        [HttpPost]
        [Route("search_cities/{cities}")]
        public async Task<IActionResult> SerchWeatherCitiesApi([FromRoute] string cities)
        {
            string[] citiesArr = GetCitiesSplitedWithSeparator(cities);
            var _apiService = new ConnectWeatherApiService(new RestClient());

            var response = await _apiService.ConsumeWeatherApiService(citiesArr);
            return await ProcessWeathersList(response);
        }

        private async Task<IActionResult> ProcessWeathersList(RootObject response)
        {
            if (response == null || response.weathersList == null || !response.weathersList.Any())
                return NotFound(JsonConvert.SerializeObject(response));

            if ("Success".Equals(response.messageResponse))
            {
                _apiService.CreateMany(response.weathersList);
                response.messageResponse += $"weathers created in db from weathers returned from api";

                return Ok(JsonConvert.SerializeObject(response));
            }
            else
                return BadRequest(JsonConvert.SerializeObject(response));
        }

        private static string[] GetCitiesSplitedWithSeparator(string cities)
        {
            var citiesArr = cities.Split(',');
            if (citiesArr.Length == 0)
                citiesArr = cities.Split(';');

            return citiesArr;
        }
    }
}