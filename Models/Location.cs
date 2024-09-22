using System;
using System.Collections.Generic;

namespace WeatherApplication.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public int? UserId { get; set; }

    public string CurrentLocation { get; set; } = null!;

    public string SearchLocation { get; set; } = null!;

    public virtual ICollection<AirQualityIndex> AirQualityIndices { get; set; } = new List<AirQualityIndex>();

    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    public virtual ICollection<DailyForecast> DailyForecasts { get; set; } = new List<DailyForecast>();

    public virtual ICollection<HourlyForecast> HourlyForecasts { get; set; } = new List<HourlyForecast>();

    public virtual ICollection<SunriseSunsetTime> SunriseSunsetTimes { get; set; } = new List<SunriseSunsetTime>();

    public virtual User? User { get; set; }

    public virtual ICollection<WeatherDatum> WeatherData { get; set; } = new List<WeatherDatum>();

    public virtual ICollection<WeatherHistory> WeatherHistories { get; set; } = new List<WeatherHistory>();
}
