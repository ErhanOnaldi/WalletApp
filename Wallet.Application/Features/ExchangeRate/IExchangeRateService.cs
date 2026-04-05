using Wallet.Application.Features.ExchangeRate.DTOs;

namespace Wallet.Application.Features.ExchangeRate;

public interface IExchangeRateService
{
    Task<decimal?> ConvertAsync(ExchangeRateRequest request);
    Task<decimal?> GetExchangeRateAsync(ExchangeRateRequest request);
}