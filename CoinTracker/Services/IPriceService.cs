namespace CoinTracker.Services;


public interface IPriceService
{
    decimal CalculateEstimatedValue(decimal denomination);
}