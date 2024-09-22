using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeatherApplication.Models
{
    public partial class WeatherDatum
    {
        [Key]
        public int WeatherId { get; set; }

        public int? LocationId { get; set; }

        public double Temperature { get; set; }

        public string WeatherIcon { get; set; } = null!;

        public double Humidity { get; set; }

        public double WindSpeed { get; set; }

        public double UvIndex { get; set; }

        public DateTime? RecordedAt { get; set; }

        public virtual Location? Location { get; set; }
    }
}
