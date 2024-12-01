using CurrencyTracker.Models;
using CurrencyTracker.Models.Entities;
using Flurl;
using Flurl.Http;
using Microsoft.EntityFrameworkCore;

namespace CurrencyTracker.Services;

public interface IExchangeRatesService
{
    Task<ExchangeRate> GetOrUpdateExchangeRateAsync(string currency, string date);
}

public class ExchangeRatesService(CurrencyTrackerDbContext dbContext) : IExchangeRatesService
{
    private readonly CurrencyTrackerDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<ExchangeRate> GetOrUpdateExchangeRateAsync(string currency, string date)
    {
        if (!DateTime.TryParse(date, out var parsedDate))
            throw new ArgumentException("Invalid date format. Use the format yyyy-MM-dd.", nameof(date));

        var existingRate = _dbContext.ExchangeRates
            .FirstOrDefault(rate => rate.Code.ToLower() == currency.ToLower() && rate.Date == parsedDate);
        if (existingRate != null) 
            return existingRate;

        try
        {
            var response = await $"https://api.nbp.pl/api/exchangerates/rates/C/{currency}/{date}/"
                .SetQueryParam("format", "json")
                .GetJsonAsync<NbpResponse>();

            if (response.Rates == null || !response.Rates.Any())
                throw new KeyNotFoundException("No exchange rate data was found for the specified currency and date.");

            var newRate = new ExchangeRate
            {
                Currency = response.Currency,
                Code = response.Code,
                Date = DateTime.Parse(response.Rates[0].EffectiveDate),
                BuyRate = response.Rates[0].Bid,
                SellRate = response.Rates[0].Ask
            };

            _dbContext.ExchangeRates.Add(newRate);
            await _dbContext.SaveChangesAsync();

            return newRate;
        }
        catch (FlurlHttpException ex)
        {
            throw new Exception($"An error occurred while fetching data: {ex.StatusCode}. {ex}");
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while updating the database.", dbEx);
        }
    }
}
