using CoinTracker.Data;
using CoinTracker.Models;
using Microsoft.EntityFrameworkCore;


namespace CoinTracker.Repositories;


public class CoinRepository : ICoinRepository
{
    private readonly ApplicationDbContext _context;
    public CoinRepository(ApplicationDbContext context) => _context = context;


    public async Task<IEnumerable<Coin>> GetAllAsync() => await _context.Coins.ToListAsync();
    public async Task<Coin?> GetByIdAsync(int id) => await _context.Coins.FindAsync(id);


    public async Task AddAsync(Coin coin)
    {
        _context.Coins.Add(coin);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAsync(Coin coin)
    {
        _context.Coins.Update(coin);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var coin = await _context.Coins.FindAsync(id);
        if (coin != null)
        {
            _context.Coins.Remove(coin);
            await _context.SaveChangesAsync();
        }
    }
}