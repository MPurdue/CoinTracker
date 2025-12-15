using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CoinTracker.Models;

namespace CoinTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Coin> Coins { get; set; }
        public DbSet<CoinPrice> CoinPrices { get; set; }
    }
}
