using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoinTracker.Models.ViewModels
{
    public class CoinImportViewModel
    {
        [Required]
        public required IFormFile ExcelFile { get; set; }
    }
}
