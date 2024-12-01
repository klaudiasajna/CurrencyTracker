using CurrencyTracker.Models.Entities;
using CurrencyTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyTracker
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController(IExchangeRatesService exchangeRatesService) : ControllerBase
    {
        // GET api/nbp/rate/{currency}/{date}
        [HttpGet("rate/{currency}/{date}")]
        public async Task<ActionResult<ExchangeRate>> GetRate(string currency, string date)
        {
            try
            {
                var rate = await exchangeRatesService.GetOrUpdateExchangeRateAsync(currency, date);
                return Ok(rate);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}