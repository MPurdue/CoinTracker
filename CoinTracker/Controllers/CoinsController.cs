using CoinTracker.Models;
using CoinTracker.Models.ViewModels;
using CoinTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;

namespace CoinTracker.Controllers
{
    [Authorize]
    public class CoinsApiController : Controller
    {
        private readonly ICoinRepository _repo;

        public CoinsApiController(ICoinRepository repo)
        {
            _repo = repo;
        }

        // GET: /Coins
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var coins = await _repo.GetAllAsync();
            return View(coins);
        }

        // GET: /Coins/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Coins/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coin coin)
        {
            if (!ModelState.IsValid)
                return View(coin);

            await _repo.AddAsync(coin);
            return RedirectToAction(nameof(Index));
        }

        // ============================
        // EXCEL IMPORT
        // ============================

        // GET: /Coins/Import
        [Authorize(Roles = "Admin")]
        public IActionResult Import()
        {
            return View();
        }

        // POST: /Coins/Import
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(CoinImportViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using var stream = model.ExcelFile.OpenReadStream();
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);

            var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header row

            foreach (var row in rows)
            {
                // Your Excel columns: Denomination, Name, Mint, Year, Grade, Notes, Link
                var coin = new Coin
                {
                    Denomination = row.Cell(1).TryGetValue<decimal>(out var denom) ? denom : 0m,
                    Name = row.Cell(2).GetValue<string>(),
                    Mint = row.Cell(3).GetValue<string>(),
                    Year = row.Cell(4).TryGetValue<int>(out var year) ? year : null,
                    Grade = row.Cell(5).GetValue<string>(),
                    Notes = row.Cell(6).GetValue<string>(),
                    ReferenceUrl = row.Cell(13).GetValue<string>()
                };

                await _repo.AddAsync(coin);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}