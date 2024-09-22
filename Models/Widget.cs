using System;
using System.Collections.Generic;

namespace WeatherApplication.Models;

public partial class Widget
{
    public int WidgetId { get; set; }

    public int? UserId { get; set; }

    public string WidgetType { get; set; } = null!;

    public string Settings { get; set; } = null!;

    public virtual User? User { get; set; }
}
