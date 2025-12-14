using System.ComponentModel.DataAnnotations;


namespace CoinTracker.Models;


public class Coin
{
    public int CoinId { get; set; }


    [Required]
    public decimal Denomination { get; set; }


    [Required, StringLength(200)]
    public string Name { get; set; } = string.Empty;


    [Required, StringLength(5)]
    public string Mint { get; set; } = string.Empty;


    [Required]
    public int Year { get; set; }


    [StringLength(20)]
    public string? Grade { get; set; }


    [StringLength(500)]
    public string? Notes { get; set; }


    [Url]
    public string? ReferenceUrl { get; set; }
}