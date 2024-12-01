using CurrencyTracker.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyTracker;

public class CurrencyTrackerDbContext : DbContext
{
    public CurrencyTrackerDbContext(DbContextOptions<CurrencyTrackerDbContext> options) : base(options) { }

    public DbSet<ExchangeRate> ExchangeRates { get; set; }
}
