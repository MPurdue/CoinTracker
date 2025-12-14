using CoinTracker.Models;
using Microsoft.Extensions.Caching.Memory;


namespace CoinTracker.Repositories;


public class CachedCoinRepository : ICoinRepository
{
    private readonly ICoinRepository _inner;
    private readonly IMemoryCache _cache;


    public CachedCoinRepository(CoinRepository inner, IMemoryCache cache)
    {
        _inner = inner;
        _cache = cache;
    }


    public async Task<IEnumerable<Coin>> GetAllAsync()
    => await _cache.GetOrCreateAsync("coins", e => _inner.GetAllAsync())!;


    public Task<Coin?> GetByIdAsync(int id) => _inner.GetByIdAsync(id);
    public Task AddAsync(Coin coin) => _inner.AddAsync(coin);
    public Task UpdateAsync(Coin coin) => _inner.UpdateAsync(coin);
    public Task DeleteAsync(int id) => _inner.DeleteAsync(id);
}