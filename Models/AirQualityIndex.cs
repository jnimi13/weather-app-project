using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Make sure to include this for the [Key] attribute

namespace WeatherApplication.Models
{
    public partial class AirQualityIndex
    {
        [Key]
        public int AqiId { get; set; } // Primary key

        public int? LocationId { get; set; }
        public double Aqi { get; set; }
        public string Pollutants { get; set; } = null!;
        public DateTime? RecordedAt { get; set; }

        public virtual Location? Location { get; set; }
    }
}
