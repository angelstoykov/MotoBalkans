using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data;
using MotoBalkans.Data.Contracts;
using MotoBalkans.Data.Models;
using MotoBalkans.Data.Repository;
using MotoBalkans.Data.SeedDB;
using MotoBalkans.Services;
using MotoBalkans.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MotoBalkansDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<MotoBalkansDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMotoBalkansDbContext, MotoBalkansDbContext>();
builder.Services.AddScoped<IAvailabilityChecker, AvailabilityChecker>();
builder.Services.AddScoped<IRepository<Motorcycle>, Repository<Motorcycle>>();
builder.Services.AddScoped<IRepository<Engine>, Repository<Engine>>();
builder.Services.AddScoped<IRepository<Transmission>, Repository<Transmission>>();
builder.Services.AddScoped<IRepository<Rental>, Repository<Rental>>();
builder.Services.AddScoped<IRepository<Report>, Repository<Report>>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IMotorcycleService, MotorcycleService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<MotoBalkansDbContext>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

    // Perform database migrations
   // dbContext.Database.Migrate();

    // Seed default data
    SeedData.Initialize(dbContext, userManager, roleManager).Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error/NotFound");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
