using CoinTracker.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoinTracker.Controllers
{
    [ApiController]
    [Route("api/coins")]
    public class CoinsApiController : ControllerBase
    {
        private readonly ICoinRepository _coins;
        private readonly ICoinPriceRepository _prices;

        public CoinsApiController(
            ICoinRepository coins,
            ICoinPriceRepository prices)
        {
            _coins = coins;
            _prices = prices;
        }

        // GET: /api/coins
        [HttpGet]
        public async Task<IActionResult> GetCoins()
        {
            return Ok(await _coins.GetAllAsync());
        }

        // GET: /api/coins/{id}/prices
        [HttpGet("{id}/prices")]
        public async Task<IActionResult> GetPricesForCoin(int id)
        {
            return Ok(await _prices.GetByCoinIdAsync(id));
        }
    }
}
