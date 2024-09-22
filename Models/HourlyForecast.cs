using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeatherApplication.Models
{
    public partial class HourlyForecast
    {
        [Key]
        public int HourlyId { get; set; }

        public int? LocationId { get; set; }

        public double Temperature { get; set; }

        public double Precipitation { get; set; }

        public double WindSpeed { get; set; }

        public string WeatherIcon { get; set; } = null!;

        public DateTime ForecastTime { get; set; }

        public virtual Location? Location { get; set; }
    }
}
