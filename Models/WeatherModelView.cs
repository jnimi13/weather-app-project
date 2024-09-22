namespace WeatherApplication.Models
{
    public class WeatherModelView
    {
        public string CurrentDate { get; set; } = String.Empty;
        public string CurrentTime { get; set; } = String.Empty;
        public string CurrentCity { get; set; } = String.Empty;
        public string CurrentTemperature { get; set; } = String.Empty;
        public string CurrentWind { get; set; } = String.Empty;
        public string CurrentHumidity { get; set; } = String.Empty;
        public string CurrentUV { get; set; } = String.Empty;
        public string WeatherConditionImg { get; set; } = String.Empty;
        public string WeatherCondition { get; set; } = String.Empty;
        public string AlertsMessage { get; set; } = String.Empty;
        public List<Forecast> Forecasts { get; set; } = new List<Forecast>();
        public string CurrentSunrise { get; set; } = String.Empty;  
        public string CurrentSunset { get; set; } = String.Empty;
        public string CurrentAirQuality { get; set; } = String.Empty;

        public double CurrentAirQualityPM { get; set; } = 0.0f;
    }

    public class Forecast
    {
        public string Day { get; set; } = String.Empty;
        public string Icon { get; set; } = String.Empty;
        public string Condition { get; set; } = String.Empty;
        public string Temperature { get; set; } = String.Empty;
    }
}
