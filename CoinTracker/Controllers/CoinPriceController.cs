using CoinTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinTracker.Controllers
{
    [Authorize]
    public class CoinPricesController : Controller
    {
        private readonly ICoinPriceRepository _repo;

        public CoinPricesController(ICoinPriceRepository repo)
        {
            _repo = repo;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string search)
        {
            var prices = await _repo.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                prices = prices.Where(p =>
                    (p.Coin != null && p.Coin.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Coin != null && p.Coin.Year.ToString().Contains(search)) ||
                    (p.Coin != null && p.Coin.Mint.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    p.Price.ToString().Contains(search)
                );
            }

            return View(prices);
        }
    }
}