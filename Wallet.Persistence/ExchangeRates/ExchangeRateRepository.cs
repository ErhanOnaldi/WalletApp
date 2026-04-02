using Wallet.Application.Interfaces.Persistence.ExchangeRates;
using Wallet.Domain.Entities;

namespace Wallet.Persistence.ExchangeRates;

public class ExchangeRateRepository(AppDbContext dbContext) : Repository<ExchangeRate>(dbContext), IExchangeRateRepository
{
}
