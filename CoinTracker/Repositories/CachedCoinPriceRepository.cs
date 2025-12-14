using CoinTracker.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CoinTracker.Repositories;

public class CachedCoinPriceRepository : ICoinPriceRepository
{
    private readonly ICoinPriceRepository _inner;
    private readonly IMemoryCache _cache;

    public CachedCoinPriceRepository(
        CoinPriceRepository inner,
        IMemoryCache cache)
    {
        _inner = inner;
        _cache = cache;
    }

    public async Task<CoinPrice?> GetLatestAsync(int coinId)
    {
        return await _cache.GetOrCreateAsync(
            $"coinprice_{coinId}",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(5);
                return await _inner.GetLatestAsync(coinId);
            });
    }

    public Task AddAsync(CoinPrice price)
        => _inner.AddAsync(price);
}
