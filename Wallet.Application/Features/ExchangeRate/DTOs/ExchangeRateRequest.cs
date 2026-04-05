using Wallet.Domain.Enums;

namespace Wallet.Application.Features.ExchangeRate.DTOs;

public record ExchangeRateRequest(Currency From, Currency To, decimal Amount);