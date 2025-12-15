using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinTracker.Models
{
    public class CoinTransaction
    {
        public int Id { get; set; }

        [Required]
        public int CoinId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PricePaid { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    }
}
