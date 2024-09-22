using System;
using System.Collections.Generic;

namespace WeatherApplication.Models;

public partial class UserProfileView
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ProfileData { get; set; }

    public string? Units { get; set; }

    public string? Themes { get; set; }
}
