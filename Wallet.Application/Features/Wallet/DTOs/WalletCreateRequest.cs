using Wallet.Domain.Enums;

namespace Wallet.Application.Features.Wallet.DTOs;

public record WalletCreateRequest(Currency Currency, decimal DailyTransferLimit);