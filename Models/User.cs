using System;
using System.Collections.Generic;

namespace WeatherApplication.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<ProfileManagement> ProfileManagements { get; set; } = new List<ProfileManagement>();

    public virtual ICollection<Setting> Settings { get; set; } = new List<Setting>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<VoiceAssistanceIntegration> VoiceAssistanceIntegrations { get; set; } = new List<VoiceAssistanceIntegration>();

    public virtual ICollection<Widget> Widgets { get; set; } = new List<Widget>();
}
