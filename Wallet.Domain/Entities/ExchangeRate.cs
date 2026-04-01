using Wallet.Domain.Enums;

namespace Wallet.Domain.Entities;

public class ExchangeRate
{
    int Id {get; set;}
    public Currency ToCurrency { get; set; }
    public Currency FromCurrency { get; set; }
    public decimal Rate { get; set; }
    public DateTime LastUpdated { get; set; }
    
}