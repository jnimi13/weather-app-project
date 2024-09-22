using Newtonsoft.Json;

namespace WeatherApplication.Models
{
    public class CityCoordinates
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; } = 0.0;
        
        [JsonProperty("lon")]
        public double Longitude { get; set; } = 0.0;

        public string Name { get; set; } = string.Empty;
    }
}
