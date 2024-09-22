using System;
using System.Collections.Generic;

namespace WeatherApplication.Models;

public partial class CurrentWeatherView
{
    public string CurrentLocation { get; set; } = null!;

    public double Temperature { get; set; }

    public string WeatherIcon { get; set; } = null!;

    public double Humidity { get; set; }

    public double WindSpeed { get; set; }

    public double UvIndex { get; set; }

    public DateTime? RecordedAt { get; set; }
}
