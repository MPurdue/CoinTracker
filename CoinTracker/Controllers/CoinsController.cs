using CoinTracker.Models;
using CoinTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace CoinTracker.Controllers;

public class CoinsController : Controller
{
    private readonly ICoinRepository _repo;

    public CoinsController(ICoinRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _repo.GetAllAsync());
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Import()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Import(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ModelState.AddModelError("", "Please upload an Excel file.");
            return View();
        }

        using var stream = file.OpenReadStream();
        using var package = new ExcelPackage(stream);
        var worksheet = package.Workbook.Worksheets[0];

        for (int row = 2; row <= worksheet.Dimension.Rows; row++)
        {
            var coin = new Coin
            {
                Denomination = decimal.Parse(worksheet.Cells[row, 1].Text),
                Name = worksheet.Cells[row, 2].Text,
                Mint = worksheet.Cells[row, 3].Text,
                Year = int.Parse(worksheet.Cells[row, 4].Text),
                Grade = worksheet.Cells[row, 5].Text,
                Notes = worksheet.Cells[row, 6].Text,
                ReferenceUrl = worksheet.Cells[row, 7].Text
            };

            await _repo.AddAsync(coin);
        }

        return RedirectToAction(nameof(Index));
    }
}
