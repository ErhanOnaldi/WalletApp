using Wallet.Domain.Entities;
using Wallet.Domain.Enums;

namespace Wallet.Application.Interfaces.Persistence.ExchangeRates;

public interface IExchangeRateRepository : IRepository<ExchangeRate>
{
    Task<Domain.Entities.ExchangeRate?> GetExchangeRate(Currency from, Currency to);
}
