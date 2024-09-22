using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeatherApplication.Models
{
    public partial class ProfileManagement
    {
        [Key]
        public int ProfileId { get; set; }

        public int? UserId { get; set; }

        public string ProfileData { get; set; } = null!;

        public virtual User? User { get; set; }
    }
}
