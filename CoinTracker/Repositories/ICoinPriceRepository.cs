using CoinTracker.Models;


namespace CoinTracker.Repositories;


public interface ICoinPriceRepository
{
    Task<CoinPrice?> GetLatestAsync(int coinId);
    Task AddAsync(CoinPrice price);
}