using Wallet.Domain.Enums;

namespace Wallet.Application.Features.Transfer.DTOs;

public record TransferRequest(Guid IdempotencyKey, Guid ToWalletId, Guid FromWalletId, decimal Amount, string? Description, TransferStatus Status = TransferStatus.Completed);