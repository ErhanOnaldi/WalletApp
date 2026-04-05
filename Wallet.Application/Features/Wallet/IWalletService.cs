using Wallet.Application.Features.Auth.DTOs;
using Wallet.Application.Features.Wallet.DTOs;

namespace Wallet.Application.Features.Wallet;

public interface IWalletService
{
    Task<ServiceResult<List<WalletGetByIdResponse>>> GetAllWalletListAsync(Guid userId);
    Task<ServiceResult<WalletGetByIdResponse>> GetWalletByIdAsync(Guid walletId, Guid userId);
    Task<ServiceResult<Guid>> CreateWalletAsync(Guid userId, WalletCreateRequest request);
    Task<ServiceResult> DeleteWalletAsync(Guid userId, Guid walletId);
    Task<ServiceResult> UpdateWalletAsync(Guid walletId, Guid userId, WalletUpdateRequest request);
    Task<ServiceResult> DepositAsync(Guid userId, Guid walletId, WalletDepositWithdrawalRequest depositRequest);
    Task<ServiceResult> WithdrawalAsync(Guid userId, Guid walletId, WalletDepositWithdrawalRequest withdrawalRequest);
    
}