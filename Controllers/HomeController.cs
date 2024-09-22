using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApplication.Models;
using WeatherApplication.Services;

namespace WeatherApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IWeatherService _weatherService;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IWeatherService weatherService )
        {
            _logger = logger;
            _httpClient = httpClient;
            _weatherService = weatherService;
        }

        public async Task<IActionResult> IndexAsync([FromQuery] string city = "Mexico City")
        {
            WeatherResponse cityWeather = await _weatherService.GetWeatherForCityAsync(city);
            if (cityWeather != default)
            {

                WeatherForecastResponse response = await _weatherService.GetForecastAsync(cityWeather.Coord.Lat, cityWeather.Coord.Lon);

                var weathermodel = new WeatherModelView()
                {
                    CurrentCity = cityWeather.Name,
                    CurrentAirQualityPM = 6.66,
                    CurrentAirQuality = "Poor",
                    CurrentHumidity = "20",
                    CurrentUV = "Unknown",
                    AlertsMessage = "Unknown",
                    CurrentSunrise = "5:45",
                    CurrentSunset = "19:00",
                    CurrentTemperature = $"{(int)cityWeather.Main.Temp}",
                    CurrentWind = "24.08km/h",
                    WeatherCondition = cityWeather.Weather[0].Description,
                    WeatherConditionImg = GetWeatherIcon(cityWeather.Weather[0]),
                    Forecasts = response.Forecasts.Take(5).Select(x =>
                    {
                        var dateTime = DateTimeOffset.FromUnixTimeSeconds(x.Dt).LocalDateTime;
                        return new Forecast { Day = dateTime.DayOfWeek.ToString(), Condition = x.Weather[0].Description, Temperature = ((int)x.Main.Temp).ToString(), Icon = GetWeatherIcon(x.Weather[0]) };
                    }).ToList()
                };
                return View(weathermodel);
            }
            else
            {
                var weathermodel = new WeatherModelView()
                {
                    CurrentCity = $"Unknown City '{city}'"
                };
                return View(weathermodel);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static string[] imageSource = ["weather_cloud_clouds_cloudy_icon.png", "weather_clouds_cloudy_forecast_rain_icon.png", "weather_clouds_cloudy_rain_sunny_icon.png",
    "weather_clouds_night_storm_icon.png", "weather_clouds_snow_winter_icon.png", "weather_clouds_sun_sunny_icon.png", "weather_hurricane_storm_tornado_icon.png",
    "weather_sun_sunny_temperature_icon.png", "weather_wind_windy_icon.png"];

        private string GetWeatherIcon(Weather weather)
        {
            string img = "images/weather-conditions/";
            int weatherId = weather.Id;

            if (weatherId >= 200 && weatherId <= 232)
            {
                img += imageSource[3]; // Thunderstorm
            }
            else if (weatherId >= 300 && weatherId <= 321)
            {
                img += imageSource[1]; // Drizzle
            }
            else if (weatherId >= 500 && weatherId <= 531)
            {
                img += imageSource[1]; // Rain
            }
            else if (weatherId >= 600 && weatherId <= 622)
            {
                img += imageSource[4]; // Snow
            }
            else if (weatherId >= 802 && weatherId <= 804)
            {
                img += imageSource[0]; // Clouds
            }
            else if (weatherId == 801)
            {
                img += imageSource[5]; // Few Clouds
            }
            else
            {
                img += imageSource[7]; // Default or unknown condition
            }

            return img;
        }
    }
}
