using System;
using System.Collections.Generic;

namespace WeatherApplication.Models;

public partial class Setting
{
    public int SettingId { get; set; }

    public int? UserId { get; set; }

    public string Units { get; set; } = null!;

    public string Themes { get; set; } = null!;

    public string LocationSettings { get; set; } = null!;

    public virtual User? User { get; set; }
}
