using CoinTracker.Models;

namespace CoinTracker.Repositories
{
    public interface ICoinPriceRepository
    {
        Task<IEnumerable<CoinPrice>> GetAllAsync();
        Task<IEnumerable<CoinPrice>> GetByCoinIdAsync(int coinId);
        Task AddAsync(CoinPrice price);
    }
}
