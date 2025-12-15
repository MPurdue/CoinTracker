using CoinTracker.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CoinTracker.Repositories
{
    public class CachedCoinPriceRepository : ICoinPriceRepository
    {
        private readonly ICoinPriceRepository _inner;
        private readonly IMemoryCache _cache;

        private const string ALL_KEY = "prices_all";

        public CachedCoinPriceRepository(
            ICoinPriceRepository inner,
            IMemoryCache cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task<IEnumerable<CoinPrice>> GetAllAsync()
        {
            return await _cache.GetOrCreateAsync(ALL_KEY, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return await _inner.GetAllAsync();
            }) ?? Enumerable.Empty<CoinPrice>();
        }

        public async Task<IEnumerable<CoinPrice>> GetByCoinIdAsync(int coinId)
        {
            string key = $"prices_coin_{coinId}";

            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return await _inner.GetByCoinIdAsync(coinId);
            }) ?? Enumerable.Empty<CoinPrice>();
        }

        public async Task AddAsync(CoinPrice price)
        {
            await _inner.AddAsync(price);
            _cache.Remove(ALL_KEY);
            _cache.Remove($"prices_coin_{price.CoinId}");
        }
    }
}
