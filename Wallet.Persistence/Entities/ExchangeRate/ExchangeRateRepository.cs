using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces.Persistence.ExchangeRates;
using Wallet.Domain.Enums;

namespace Wallet.Persistence.Entities.ExchangeRate;

public class ExchangeRateRepository(AppDbContext dbContext) : Repository<Domain.Entities.ExchangeRate>(dbContext), IExchangeRateRepository
{
    public async Task<Domain.Entities.ExchangeRate?> GetExchangeRate(Currency from, Currency to)
    {
        return await DbContext.ExchangeRates.Where(x => x.FromCurrency == from && x.ToCurrency == to).FirstOrDefaultAsync();

    }
}
