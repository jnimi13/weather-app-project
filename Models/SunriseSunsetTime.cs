using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeatherApplication.Models
{
    public partial class SunriseSunsetTime
    {
        [Key]
        public int SunriseSunsetId { get; set; }

        public int? LocationId { get; set; }

        public TimeOnly SunriseTime { get; set; }

        public TimeOnly SunsetTime { get; set; }

        public DateOnly Date { get; set; }

        public virtual Location? Location { get; set; }
    }
}
