using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherApplication.Services;

namespace WeatherApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("air-quality")]
        public async Task<IActionResult> GetAirQuality(double latitude, double longitude)
        {
            var aqiData = await _weatherService.GetAirQualityAsync(latitude, longitude);
            return Ok(aqiData);
        }
    }
}
