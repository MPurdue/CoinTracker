namespace CoinTracker.Services;


public class PriceService : IPriceService
{
    public decimal CalculateEstimatedValue(decimal denomination)
    {
        // Simple placeholder logic – admin can override
        return denomination * 25;
    }
}