using System.Threading.Tasks;
using WeatherApplication.Models;

namespace WeatherApplication.Services
{
    public interface IWeatherService
    {
        Task<AirQualityIndex> GetAirQualityAsync(double latitude, double longitude);

        Task<WeatherForecastResponse> GetForecastAsync(double latitude, double longitude);

        Task<WeatherResponse?> GetWeatherForCityAsync(string cityName);
    }
}
