using CoinTracker.Models;


namespace CoinTracker.Repositories;


public interface ICoinRepository
{
    Task<IEnumerable<Coin>> GetAllAsync();
    Task<Coin?> GetByIdAsync(int id);
    Task AddAsync(Coin coin);
    Task UpdateAsync(Coin coin);
    Task DeleteAsync(int id);
}