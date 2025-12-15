using CoinTracker.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CoinTracker.Repositories
{
    public class CachedCoinRepository : ICoinRepository
    {
        private readonly ICoinRepository _inner;
        private readonly IMemoryCache _cache;

        private const string CacheKey = "coins_all";

        public CachedCoinRepository(ICoinRepository inner, IMemoryCache cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task<IEnumerable<Coin>> GetAllAsync()
        {
            if (!_cache.TryGetValue(CacheKey, out IEnumerable<Coin>? coins))
            {
                coins = await _inner.GetAllAsync();
                _cache.Set(CacheKey, coins, TimeSpan.FromMinutes(5));
            }

            return coins!;
        }

        public async Task<Coin?> GetByIdAsync(int id)
            => await _inner.GetByIdAsync(id);

        public async Task AddAsync(Coin coin)
        {
            await _inner.AddAsync(coin);
            _cache.Remove(CacheKey);
        }

        public async Task UpdateAsync(Coin coin)
        {
            await _inner.UpdateAsync(coin);
            _cache.Remove(CacheKey);
        }

        public async Task DeleteAsync(int id)
        {
            await _inner.DeleteAsync(id);
            _cache.Remove(CacheKey);
        }
    }
}
