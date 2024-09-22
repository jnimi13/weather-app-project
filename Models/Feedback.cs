using System;
using System.Collections.Generic;

namespace WeatherApplication.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? UserId { get; set; }

    public string FeedbackText { get; set; } = null!;

    public DateTime? SubmittedAt { get; set; }

    public virtual User? User { get; set; }
}
