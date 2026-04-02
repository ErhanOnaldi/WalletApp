using Wallet.Application.Interfaces.Persistence.ExchangeRates;

namespace Wallet.Persistence.Entities.ExchangeRate;

public class ExchangeRateRepository(AppDbContext dbContext) : Repository<Domain.Entities.ExchangeRate>(dbContext), IExchangeRateRepository
{
}
