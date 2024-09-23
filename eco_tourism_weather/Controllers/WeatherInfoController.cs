using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using eco_tourism_weather.Services;
using eco_tourism_weather.Models;
using Microsoft.AspNetCore.Authorization;

namespace eco_tourism_weather.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WeatherInfoController : ControllerBase
    {
        private readonly IWeatherInfoService _weatherInfoService;

        public WeatherInfoController(IWeatherInfoService weatherInfoService) =>
            _weatherInfoService = weatherInfoService;
            
        [HttpGet("{cityName}")]
        public async Task<IActionResult> GetWeather(string cityName)
        {
            var weatherInfo = await _weatherInfoService.GetWeatherAsync(cityName);
            return Ok(weatherInfo);
        }
    }
}
