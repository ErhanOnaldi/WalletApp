using Wallet.Application.Features.ExchangeRate.DTOs;
using Wallet.Application.Interfaces.Persistence.ExchangeRates;

namespace Wallet.Application.Features.ExchangeRate;

public class ExchangeRateService(IExchangeRateRepository exchangeRateRepository) : IExchangeRateService
{
    public async Task<decimal?> ConvertAsync(ExchangeRateRequest request)
    {
        if (request.From.Equals(request.To))
        {
            return request.Amount;
        }
        var exchangeRate = await exchangeRateRepository.GetExchangeRate(request.From, request.To);
        if (exchangeRate is null)
        {
            return null;
        }
        return exchangeRate.Rate * request.Amount;
    }

    public async Task<decimal?> GetExchangeRateAsync(ExchangeRateRequest request)
    {
        if (request.From.Equals(request.To))
        {
            return 1;
        }
        var exchangeRate = await exchangeRateRepository.GetExchangeRate(request.From, request.To);
        if (exchangeRate is null)
        {
            return null;
        }
        return exchangeRate.Rate;
    }
}