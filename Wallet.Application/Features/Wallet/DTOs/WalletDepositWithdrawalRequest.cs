using Wallet.Domain.Enums;

namespace Wallet.Application.Features.Wallet.DTOs;

public record WalletDepositWithdrawalRequest(string Description, decimal Amount);