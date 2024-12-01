using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CurrencyTracker.Models.Entities;

[Table("exchange_rates")]
public class ExchangeRate
{
    [JsonIgnore]
    public int Id { get; set; } 
    public string Currency { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double BuyRate { get; set; }
    public double SellRate { get; set; } 
}