using System.ComponentModel.DataAnnotations;

namespace CoinTracker.Models.ViewModels
{
    public class CoinImportViewModel
    {
        [Required]
        [Display(Name = "Excel File")]
        public IFormFile ExcelFile { get; set; } = null!;
    }
}
