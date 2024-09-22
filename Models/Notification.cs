using System;
using System.Collections.Generic;

namespace WeatherApplication.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public string NotificationType { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
