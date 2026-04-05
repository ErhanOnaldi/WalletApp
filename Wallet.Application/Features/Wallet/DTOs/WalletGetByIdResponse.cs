using Wallet.Domain.Enums;

namespace Wallet.Application.Features.Wallet.DTOs;

public record WalletGetByIdResponse(Guid Id, Guid UserId, Currency Currency, decimal Balance, decimal DailyTransferLimit, DateTime CreatedAt);