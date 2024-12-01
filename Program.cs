using CurrencyTracker;
using CurrencyTracker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        
        builder.Services.AddDbContext<CurrencyTrackerDbContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("CurrencyTrackerDb");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
        });

        builder.Services.AddScoped<IExchangeRatesService, ExchangeRatesService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.MapControllers();
        app.Run();
    }
}