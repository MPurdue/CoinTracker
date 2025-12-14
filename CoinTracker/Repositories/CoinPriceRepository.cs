using CoinTracker.Data;
using CoinTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CoinTracker.Repositories;

public class CoinPriceRepository : ICoinPriceRepository
{
    private readonly ApplicationDbContext _context;

    public CoinPriceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CoinPrice?> GetLatestAsync(int coinId)
    {
        return await _context.CoinPrices
            .Where(p => p.CoinId == coinId)
            .OrderByDescending(p => p.LastUpdated)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(CoinPrice price)
    {
        _context.CoinPrices.Add(price);
        await _context.SaveChangesAsync();
    }
}
