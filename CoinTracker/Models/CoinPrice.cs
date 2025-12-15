using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinTracker.Models
{
    public class CoinPrice
    {
        [Key]
        [Column("CoinPriceId")]
        public int Id { get; set; }

        public int CoinId { get; set; }

        [Column("EstimatedValue", TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Column("LastUpdated")]
        public DateTime RecordedAt { get; set; }

        public Coin Coin { get; set; } = null!;
    }
}