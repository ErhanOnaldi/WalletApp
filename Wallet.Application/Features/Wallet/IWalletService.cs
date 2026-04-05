using Wallet.Application.Features.Auth.DTOs;
using Wallet.Application.Features.Wallet.DTOs;

namespace Wallet.Application.Features.Wallet;

public interface IWalletService
{
    Task<ServiceResult<List<WalletGetByIdResponse>>> GetAllWalletList(Guid userId);
}