using System.ComponentModel.DataAnnotations;


namespace CoinTracker.Models;


public class CoinPrice
{
    public int CoinPriceId { get; set; }
    public int CoinId { get; set; }


    [Required]
    public decimal EstimatedValue { get; set; }


    public DateTime LastUpdated { get; set; }


    public Coin Coin { get; set; } = null!;
}