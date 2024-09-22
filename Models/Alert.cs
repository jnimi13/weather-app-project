using System;
using System.Collections.Generic;

namespace WeatherApplication.Models;

public partial class Alert
{
    public int AlertId { get; set; }

    public int? LocationId { get; set; }

    public string AlertType { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime? IssuedAt { get; set; }

    public virtual Location? Location { get; set; }
}
