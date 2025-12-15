using CoinTracker.Data;
using CoinTracker.Repositories;
using CoinTracker.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Database (SQL Server)
// ----------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ----------------------------
// Identity
// ----------------------------
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

// ----------------------------
// MVC + Cache
// ----------------------------
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();

// ----------------------------
// Repositories (cached decorators)
// ----------------------------
builder.Services.AddScoped<CoinRepository>();
builder.Services.AddScoped<ICoinRepository>(sp =>
    new CachedCoinRepository(
        sp.GetRequiredService<CoinRepository>(),
        sp.GetRequiredService<IMemoryCache>()));

builder.Services.AddScoped<CoinPriceRepository>();
builder.Services.AddScoped<ICoinPriceRepository>(sp =>
    new CachedCoinPriceRepository(
        sp.GetRequiredService<CoinPriceRepository>(),
        sp.GetRequiredService<IMemoryCache>()));

// ----------------------------
// Services
// ----------------------------
builder.Services.AddHttpClient();
builder.Services.AddScoped<IPriceService, PriceService>();

var app = builder.Build();

// ----------------------------
// Middleware
// ----------------------------
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();