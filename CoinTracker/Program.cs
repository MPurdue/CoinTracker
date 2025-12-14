using CoinTracker.Data;
using CoinTracker.Repositories;
using CoinTracker.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();


// Repositories
builder.Services.AddScoped<ICoinRepository, CoinRepository>();
builder.Services.AddScoped<ICoinRepository, CachedCoinRepository>();
builder.Services.AddScoped<ICoinPriceRepository, CoinPriceRepository>();
builder.Services.AddScoped<ICoinPriceRepository, CachedCoinPriceRepository>();


// Services
builder.Services.AddHttpClient();
builder.Services.AddScoped<IPriceService, PriceService>();


var app = builder.Build();


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