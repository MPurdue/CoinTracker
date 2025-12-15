using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinTracker.Models
{
    public class Coin
    {
        [Key]
        [Column("CoinId")]
        public int Id { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Denomination { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(5)]
        public string Mint { get; set; } = string.Empty;

        public int? Year { get; set; }

        [StringLength(100)]  // Increased from 20 to 100
        public string Grade { get; set; } = string.Empty;

        [StringLength(1000)]  // Increased from 500 to 1000
        public string Notes { get; set; } = string.Empty;

        [Url]
        [StringLength(500)]
        public string ReferenceUrl { get; set; } = string.Empty;

        // Optional metadata
        [StringLength(50)]
        public string Symbol { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
    }
}