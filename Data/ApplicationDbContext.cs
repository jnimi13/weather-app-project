using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeatherApplication.Models; // Add this using directive to access your models

namespace WeatherApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<WeatherDatum> WeatherData { get; set; } = null!;
        public DbSet<DailyForecast> DailyForecasts { get; set; } = null!;
        public DbSet<HourlyForecast> HourlyForecasts { get; set; } = null!;
        public DbSet<AirQualityIndex> AirQualityIndexes { get; set; } = null!;
        public DbSet<Alert> Alerts { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<Feedback> Feedbacks { get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        public DbSet<SunriseSunsetTime> SunriseSunsetTimes { get; set; } = null!;
        public DbSet<VoiceAssistanceIntegration> VoiceAssistanceIntegrations { get; set; } = null!;
        public DbSet<WeatherHistory> WeatherHistories { get; set; } = null!;
        public DbSet<Widget> Widgets { get; set; } = null!;
        public DbSet<Setting> Settings { get; set; } = null!;
        public DbSet<ProfileManagement> ProfileManagements { get; set; } = null!;
    }
}
