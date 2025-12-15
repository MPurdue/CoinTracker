using Microsoft.Extensions.Caching.Memory;
using CoinTracker.Models;

namespace CoinTracker.Repositories
{
    public class CachedCoinRepository : ICoinRepository
    {
        private readonly ICoinRepository _inner;
        private readonly IMemoryCache _cache;

        private const string CACHE_KEY = "coins_all";

        public CachedCoinRepository(
            ICoinRepository inner,
            IMemoryCache cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task<IEnumerable<Coin>> GetAllAsync()
        {
            if (!_cache.TryGetValue(CACHE_KEY, out IEnumerable<Coin> coins))
            {
                coins = await _inner.GetAllAsync();
                _cache.Set(CACHE_KEY, coins, TimeSpan.FromMinutes(5));
            }

            return coins;
        }

        public Task<Coin?> GetByIdAsync(int id)
            => _inner.GetByIdAsync(id);

        public async Task AddAsync(Coin coin)
        {
            await _inner.AddAsync(coin);
            _cache.Remove(CACHE_KEY);
        }

        public async Task UpdateAsync(Coin coin)
        {
            await _inner.UpdateAsync(coin);
            _cache.Remove(CACHE_KEY);
        }

        public async Task DeleteAsync(int id)
        {
            await _inner.DeleteAsync(id);
            _cache.Remove(CACHE_KEY);
        }
    }
}
