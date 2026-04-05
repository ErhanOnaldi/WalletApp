using Wallet.Application.Features.Auth.DTOs;
using Wallet.Application.Features.Wallet.DTOs;
using Wallet.Application.Interfaces.Persistence;
using Wallet.Application.Interfaces.Persistence.Users;
using Wallet.Application.Interfaces.Persistence.Wallets;
using WalletEntity = Wallet.Domain.Entities.Wallet;

namespace Wallet.Application.Features.Wallet;

public class WalletService(IWalletRepository walletRepository, IUserRepository userRepository,IUnitOfWork unitOfWork) : IWalletService
{
    
    public async Task<ServiceResult<List<WalletGetByIdResponse>>> GetAllWalletList(Guid userId)
    {
        var wallets = await walletRepository.GetAllWalletsAsync(userId);
        var walletsAsDto = wallets.Select(x => new WalletGetByIdResponse(x.Id, x.UserId, x.Currency, x.Balance, x.DailyTransferLimit, x.CreatedAt)).ToList();
        return ServiceResult<List<WalletGetByIdResponse>>.Success(walletsAsDto);
    }

    public async Task<ServiceResult<WalletGetByIdResponse>> GetWalletById(Guid walletId, Guid userId)
    {
        var wallet = await walletRepository.GetByIdAsync(walletId);
        if (wallet is null)
        {
            return ServiceResult<WalletGetByIdResponse>.Fail("No wallet found");
        }
        if (wallet.UserId != userId)
        {
            return ServiceResult<WalletGetByIdResponse>.Fail("Wallet does not belong to user");
        }
        var walletAsDto = new WalletGetByIdResponse(wallet.Id,wallet.UserId, wallet.Currency,wallet.Balance,wallet.DailyTransferLimit,wallet.CreatedAt);
        return ServiceResult<WalletGetByIdResponse>.Success(walletAsDto);
    }

    public async Task<ServiceResult<Guid>> CreateWallet(Guid userId,WalletCreateRequest request)
    {
        var wallet = new WalletEntity()
        {
            DailyTransferLimit = request.DailyTransferLimit,
            Balance = 0,
            Currency = request.Currency,
            UserId = userId,
            IsActive = true,
        };
        await walletRepository.AddAsync(wallet);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<Guid>.SuccessAsCreated(wallet.Id,$"api/wallets/{wallet.Id}");
    }

    public async Task<ServiceResult> DeleteWallet(Guid userId, Guid walletId)
    {
        var wallet = await walletRepository.GetByIdAsync(walletId);
        if (wallet is null)
        {
            return ServiceResult.Fail("No wallet found");
        }
        if (wallet.UserId != userId)
        {
            return ServiceResult.Fail("User does not own the wallet");
        }

        if (wallet.Balance is not 0)
        {
            return ServiceResult.Fail("Can't delete wallet with balance");
        }

        if (!wallet.IsActive)
        {
            return ServiceResult.Fail("Can't delete wallet without active");
        }
        wallet.IsActive = false;
        walletRepository.Update(wallet);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }

    public async Task<ServiceResult> UpdateWallet(Guid walletId,Guid userId, decimal dailyTransferLimit)
    {
        var wallet = await walletRepository.GetByIdAsync(walletId);
        if (wallet is null)
        {
            return ServiceResult.Fail("No wallet found");
        }

        if (wallet.UserId != userId)
        {
            return ServiceResult.Fail("User does not own the wallet");
        }
        if (wallet.DailyTransferLimit > dailyTransferLimit)
        {
            return ServiceResult.Fail("New daily transfer limit cannot be lower than the current daily transfer limit");
        }
        if (!(dailyTransferLimit <= wallet.DailyTransferLimit * 3))
        {
            return ServiceResult.Fail("The daily transfer limit cannot be updated to more than three times the previous limit.");
        }

        wallet.DailyTransferLimit = dailyTransferLimit;
        walletRepository.Update(wallet);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }
}