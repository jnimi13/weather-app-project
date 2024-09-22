using System;
using System.Collections.Generic;

namespace WeatherApplication.Models;

public partial class AirQualityIndexView
{
    public string CurrentLocation { get; set; } = null!;

    public double Aqi { get; set; }

    public string Pollutants { get; set; } = null!;

    public DateTime? RecordedAt { get; set; }
}
