using System;
using System.Collections.Generic;

namespace WeatherApplication.Models;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public int? UserId { get; set; }

    public string SubscriptionType { get; set; } = null!;

    public DateOnly ExpiryDate { get; set; }

    public virtual User? User { get; set; }
}
