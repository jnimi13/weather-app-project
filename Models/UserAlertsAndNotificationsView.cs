using System;
using System.Collections.Generic;

namespace WeatherApplication.Models;

public partial class UserAlertsAndNotificationsView
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public int? AlertId { get; set; }

    public string? AlertType { get; set; }

    public string? AlertMessage { get; set; }

    public DateTime? IssuedAt { get; set; }

    public int? NotificationId { get; set; }

    public string? NotificationType { get; set; }

    public string? NotificationMessage { get; set; }

    public DateTime? CreatedAt { get; set; }
}
