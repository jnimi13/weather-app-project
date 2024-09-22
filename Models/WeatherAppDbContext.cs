using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WeatherApplication.Models;

public partial class WeatherAppDbContext : DbContext
{
    public WeatherAppDbContext()
    {
    }

    public WeatherAppDbContext(DbContextOptions<WeatherAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AirQualityIndex> AirQualityIndices { get; set; }

    public virtual DbSet<AirQualityIndexView> AirQualityIndexViews { get; set; }

    public virtual DbSet<Alert> Alerts { get; set; }

    public virtual DbSet<CurrentWeatherView> CurrentWeatherViews { get; set; }

    public virtual DbSet<DailyForecast> DailyForecasts { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<HourlyForecast> HourlyForecasts { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<ProfileManagement> ProfileManagements { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SunriseSunsetTime> SunriseSunsetTimes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAlertsAndNotificationsView> UserAlertsAndNotificationsViews { get; set; }

    public virtual DbSet<UserProfileView> UserProfileViews { get; set; }

    public virtual DbSet<VoiceAssistanceIntegration> VoiceAssistanceIntegrations { get; set; }

    public virtual DbSet<WeatherDatum> WeatherData { get; set; }

    public virtual DbSet<WeatherHistory> WeatherHistories { get; set; }

    public virtual DbSet<Widget> Widgets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=KittyLegion\\SQLEXPRESS;Database=WeatherAppDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AirQualityIndex>(entity =>
        {
            entity.HasKey(e => e.AqiId).HasName("PK__AirQuali__F09FE5AAEA9092F7");

            entity.ToTable("AirQualityIndex");

            entity.Property(e => e.AqiId).HasColumnName("aqi_id");
            entity.Property(e => e.Aqi).HasColumnName("aqi");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Pollutants)
                .HasColumnType("text")
                .HasColumnName("pollutants");
            entity.Property(e => e.RecordedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("recorded_at");

            entity.HasOne(d => d.Location).WithMany(p => p.AirQualityIndices)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__AirQualit__locat__03F0984C");
        });

        modelBuilder.Entity<AirQualityIndexView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("AirQualityIndexView");

            entity.Property(e => e.Aqi).HasColumnName("aqi");
            entity.Property(e => e.CurrentLocation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("current_location");
            entity.Property(e => e.Pollutants)
                .HasColumnType("text")
                .HasColumnName("pollutants");
            entity.Property(e => e.RecordedAt)
                .HasColumnType("datetime")
                .HasColumnName("recorded_at");
        });

        modelBuilder.Entity<Alert>(entity =>
        {
            entity.HasKey(e => e.AlertId).HasName("PK__Alert__4B8FB03AC8968ECF");

            entity.ToTable("Alert");

            entity.Property(e => e.AlertId).HasColumnName("alert_id");
            entity.Property(e => e.AlertType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("alert_type");
            entity.Property(e => e.IssuedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("issued_at");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");

            entity.HasOne(d => d.Location).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__Alert__location___04E4BC85");
        });

        modelBuilder.Entity<CurrentWeatherView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CurrentWeatherView");

            entity.Property(e => e.CurrentLocation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("current_location");
            entity.Property(e => e.Humidity).HasColumnName("humidity");
            entity.Property(e => e.RecordedAt)
                .HasColumnType("datetime")
                .HasColumnName("recorded_at");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.UvIndex).HasColumnName("uv_index");
            entity.Property(e => e.WeatherIcon)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("weather_icon");
            entity.Property(e => e.WindSpeed).HasColumnName("wind_speed");
        });

        modelBuilder.Entity<DailyForecast>(entity =>
        {
            entity.HasKey(e => e.DailyId).HasName("PK__DailyFor__DCA5E21ED0F1469B");

            entity.ToTable("DailyForecast");

            entity.Property(e => e.DailyId).HasColumnName("daily_id");
            entity.Property(e => e.ForecastDate).HasColumnName("forecast_date");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Precipitation).HasColumnName("precipitation");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.WeatherIcon)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("weather_icon");
            entity.Property(e => e.WindSpeed).HasColumnName("wind_speed");

            entity.HasOne(d => d.Location).WithMany(p => p.DailyForecasts)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__DailyFore__locat__05D8E0BE");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__7A6B2B8C87430A67");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.FeedbackText)
                .HasColumnType("text")
                .HasColumnName("feedback_text");
            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("submitted_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedback__user_i__06CD04F7");
        });

        modelBuilder.Entity<HourlyForecast>(entity =>
        {
            entity.HasKey(e => e.HourlyId).HasName("PK__HourlyFo__46E18C80D419C823");

            entity.ToTable("HourlyForecast");

            entity.Property(e => e.HourlyId).HasColumnName("hourly_id");
            entity.Property(e => e.ForecastTime)
                .HasColumnType("datetime")
                .HasColumnName("forecast_time");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Precipitation).HasColumnName("precipitation");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.WeatherIcon)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("weather_icon");
            entity.Property(e => e.WindSpeed).HasColumnName("wind_speed");

            entity.HasOne(d => d.Location).WithMany(p => p.HourlyForecasts)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__HourlyFor__locat__07C12930");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__771831EA4DF66FDC");

            entity.ToTable("Location");

            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.CurrentLocation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("current_location");
            entity.Property(e => e.SearchLocation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("search_location");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Locations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Location__user_i__08B54D69");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842FBFBE8E66");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.NotificationType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("notification_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__user___09A971A2");
        });

        modelBuilder.Entity<ProfileManagement>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__ProfileM__AEBB701F0AD87DB1");

            entity.ToTable("ProfileManagement");

            entity.Property(e => e.ProfileId).HasColumnName("profile_id");
            entity.Property(e => e.ProfileData)
                .HasColumnType("text")
                .HasColumnName("profile_data");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ProfileManagements)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ProfileMa__user___0A9D95DB");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.SettingId).HasName("PK__Setting__256E1E32AF736F79");

            entity.ToTable("Setting");

            entity.Property(e => e.SettingId).HasColumnName("setting_id");
            entity.Property(e => e.LocationSettings)
                .HasColumnType("text")
                .HasColumnName("location_settings");
            entity.Property(e => e.Themes)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("themes");
            entity.Property(e => e.Units)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("units");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Settings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Setting__user_id__0B91BA14");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__863A7EC1FBF801EF");

            entity.ToTable("Subscription");

            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.SubscriptionType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("subscription_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Subscript__user___0C85DE4D");
        });

        modelBuilder.Entity<SunriseSunsetTime>(entity =>
        {
            entity.HasKey(e => e.SunriseSunsetId).HasName("PK__SunriseS__EE33A262C43C96DB");

            entity.Property(e => e.SunriseSunsetId).HasColumnName("sunrise_sunset_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.SunriseTime).HasColumnName("sunrise_time");
            entity.Property(e => e.SunsetTime).HasColumnName("sunset_time");

            entity.HasOne(d => d.Location).WithMany(p => p.SunriseSunsetTimes)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__SunriseSu__locat__0D7A0286");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__B9BE370FCF0FA815");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserAlertsAndNotificationsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UserAlertsAndNotificationsView");

            entity.Property(e => e.AlertId).HasColumnName("alert_id");
            entity.Property(e => e.AlertMessage)
                .HasColumnType("text")
                .HasColumnName("alert_message");
            entity.Property(e => e.AlertType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("alert_type");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IssuedAt)
                .HasColumnType("datetime")
                .HasColumnName("issued_at");
            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.NotificationMessage)
                .HasColumnType("text")
                .HasColumnName("notification_message");
            entity.Property(e => e.NotificationType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("notification_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserProfileView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UserProfileView");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.ProfileData)
                .HasColumnType("text")
                .HasColumnName("profile_data");
            entity.Property(e => e.Themes)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("themes");
            entity.Property(e => e.Units)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("units");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<VoiceAssistanceIntegration>(entity =>
        {
            entity.HasKey(e => e.VoiceId).HasName("PK__VoiceAss__128AF38138BC1AD5");

            entity.ToTable("VoiceAssistanceIntegration");

            entity.Property(e => e.VoiceId).HasColumnName("voice_id");
            entity.Property(e => e.IntegrationType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("integration_type");
            entity.Property(e => e.Settings)
                .HasColumnType("text")
                .HasColumnName("settings");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.VoiceAssistanceIntegrations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__VoiceAssi__user___0E6E26BF");
        });

        modelBuilder.Entity<WeatherDatum>(entity =>
        {
            entity.HasKey(e => e.WeatherId).HasName("PK__WeatherD__4CDA2101B004521D");

            entity.Property(e => e.WeatherId).HasColumnName("weather_id");
            entity.Property(e => e.Humidity).HasColumnName("humidity");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.RecordedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("recorded_at");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.UvIndex).HasColumnName("uv_index");
            entity.Property(e => e.WeatherIcon)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("weather_icon");
            entity.Property(e => e.WindSpeed).HasColumnName("wind_speed");

            entity.HasOne(d => d.Location).WithMany(p => p.WeatherData)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__WeatherDa__locat__0F624AF8");
        });

        modelBuilder.Entity<WeatherHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__WeatherH__096AA2E9884857E0");

            entity.ToTable("WeatherHistory");

            entity.Property(e => e.HistoryId).HasColumnName("history_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Precipitation).HasColumnName("precipitation");
            entity.Property(e => e.RecordedDate).HasColumnName("recorded_date");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.WeatherIcon)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("weather_icon");
            entity.Property(e => e.WindSpeed).HasColumnName("wind_speed");

            entity.HasOne(d => d.Location).WithMany(p => p.WeatherHistories)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__WeatherHi__locat__10566F31");
        });

        modelBuilder.Entity<Widget>(entity =>
        {
            entity.HasKey(e => e.WidgetId).HasName("PK__Widget__20C4DFC51B7AE7CB");

            entity.ToTable("Widget");

            entity.Property(e => e.WidgetId).HasColumnName("widget_id");
            entity.Property(e => e.Settings)
                .HasColumnType("text")
                .HasColumnName("settings");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WidgetType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("widget_type");

            entity.HasOne(d => d.User).WithMany(p => p.Widgets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Widget__user_id__114A936A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
