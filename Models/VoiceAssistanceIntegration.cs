using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeatherApplication.Models
{
    public partial class VoiceAssistanceIntegration
    {
        [Key]
        public int VoiceId { get; set; }

        public int? UserId { get; set; }

        public string IntegrationType { get; set; } = null!;

        public string Settings { get; set; } = null!;

        public virtual User? User { get; set; }
    }
}
