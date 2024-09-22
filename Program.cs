using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeatherApplication.Data;
using WeatherApplication.Services; // Ensure this namespace is included

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("WeatherAppDB")
    ?? throw new InvalidOperationException("Connection string 'WeatherAppDB' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

// Register HttpClient
builder.Services.AddHttpClient(); // Register HttpClient

// Register WeatherService with the IWeatherService interface
builder.Services.AddScoped<IWeatherService, WeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configure routing to go directly to the Register action
app.MapControllerRoute(
    name: "register",
    pattern: "Account/Register",
    defaults: new { controller = "Account", action = "Register" });

app.MapControllerRoute(
    name: "login",
    pattern: "Account/Login",
    defaults: new { controller = "Account", action = "Login" });

// Default route for other actions
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
