using Wallet.Domain.Enums;

namespace Wallet.Application.Features.Transfer.DTOs;

public record TransferGetResponse(
    Guid IdempotencyKey,
    Guid ToWalletId,
    Guid FromWalletId,
    TransferStatus TransferStatus,
    DateTime CreatedAt,
    DateTime CompletedAt,
    decimal Amount,
    decimal Fee,
    decimal ExchangeRate);
