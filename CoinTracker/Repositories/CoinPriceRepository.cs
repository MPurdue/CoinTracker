using CoinTracker.Data;
using CoinTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CoinTracker.Repositories
{
    public class CoinPriceRepository : ICoinPriceRepository
    {
        private readonly ApplicationDbContext _context;

        public CoinPriceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CoinPrice>> GetAllAsync()
        {
            return await _context.CoinPrices
                .Include(p => p.Coin)  // ← Add this line if missing
                .OrderByDescending(p => p.RecordedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<CoinPrice>> GetByCoinIdAsync(int coinId)
        {
            return await _context.CoinPrices
                .Include(p => p.Coin)  // ← Add this line
                .Where(p => p.CoinId == coinId)
                .OrderByDescending(p => p.RecordedAt)
                .ToListAsync();
        }

        public async Task AddAsync(CoinPrice price)
        {
            _context.CoinPrices.Add(price);
            await _context.SaveChangesAsync();
        }
    }
}