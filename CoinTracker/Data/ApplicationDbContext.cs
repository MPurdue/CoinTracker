using CoinTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CoinTracker.Data;


public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }


    public DbSet<Coin> Coins => Set<Coin>();
    public DbSet<CoinPrice> CoinPrices => Set<CoinPrice>();
}