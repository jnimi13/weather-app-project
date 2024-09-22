using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using Azure;
using Humanizer;
using Newtonsoft.Json; // Ensure you have Newtonsoft.Json installed via NuGet
using WeatherApplication.Models;

namespace WeatherApplication.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "657b507e09f23c54c538ae1dda981260"; // Replace with your actual API key

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        

        public async Task<AirQualityIndex> GetAirQualityAsync(double latitude, double longitude)
        {
            var url = $"http://api.openweathermap.org/data/2.5/air_pollution?lat={latitude}&lon={longitude}&appid={_apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<AirQualityApiResponse>(response);

            return new AirQualityIndex
            {
                Aqi = result.List[0].Main.Aqi,
                Pollutants = string.Join(", ", result.List[0].Components.Select(c => $"{c.Key}: {c.Value}")),
                RecordedAt = DateTime.UtcNow
            };
        }

        public async Task<WeatherForecastResponse> GetForecastAsync(double latitude, double longitude)
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&units=metric&appid={_apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            var weatherForecastResponse = JsonConvert.DeserializeObject<WeatherForecastResponse>(response);

            HashSet<DateOnly> dates = new HashSet<DateOnly>();
            foreach(var forecast in weatherForecastResponse.Forecasts.ToArray())
            {
                var forecastDate = DateOnly.FromDateTime(DateTimeOffset.FromUnixTimeSeconds(forecast.Dt).DateTime);
                if (dates.Contains(forecastDate))
                {
                    weatherForecastResponse.Forecasts.Remove(forecast);
                }
                else
                {
                    dates.Add(forecastDate);
                }
            }
            return weatherForecastResponse;
        }

        public async Task<WeatherResponse?> GetWeatherForCityAsync(string cityName)
        {
            var url = $"https://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=1&appid={_apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            CityCoordinates[] cityCoordinatesResponse = JsonConvert.DeserializeObject<CityCoordinates[]>(response);
            if (cityCoordinatesResponse.Length != 0)
            {

                var cityCoordinates = cityCoordinatesResponse[0];


                url = $"https://api.openweathermap.org/data/2.5/weather?lat={cityCoordinates!.Latitude}&lon={cityCoordinates.Longitude}&units=metric&exclude=daily,hourly,minutely&appid={_apiKey}";
                response = await _httpClient.GetStringAsync(url);
                var weatherModel = JsonConvert.DeserializeObject<WeatherResponse>(response);

                return weatherModel!;
            }
            else
            {
                return null;
            }
        }
    }

    public class AirQualityApiResponse
    {
        public List<AirQualityData> List { get; set; }
    }

    public class AirQualityData
    {
        public Main Main { get; set; }
        public Dictionary<string, double> Components { get; set; }
    }

    public class Main
    {
        public double Aqi { get; set; }
    }
}
