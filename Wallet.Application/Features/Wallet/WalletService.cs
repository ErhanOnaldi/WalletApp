using Wallet.Application.Features.Wallet.DTOs;
using Wallet.Application.Interfaces.Persistence;
using Wallet.Application.Interfaces.Persistence.Transactions;
using Wallet.Application.Interfaces.Persistence.Wallets;
using Wallet.Domain.Entities;
using Wallet.Domain.Enums;
using WalletEntity = Wallet.Domain.Entities.Wallet;

namespace Wallet.Application.Features.Wallet;

public class WalletService(IWalletRepository walletRepository, ITransactionRepository transactionRepository,IUnitOfWork unitOfWork) : IWalletService
{
    
    public async Task<ServiceResult<List<WalletGetByIdResponse>>> GetAllWalletListAsync(Guid userId)
    {
        var wallets = await walletRepository.GetAllWalletsAsync(userId);
        var walletsAsDto = wallets.Select(x => new WalletGetByIdResponse(x.Id, x.UserId, x.Currency, x.Balance, x.DailyTransferLimit, x.CreatedAt)).ToList();
        return ServiceResult<List<WalletGetByIdResponse>>.Success(walletsAsDto);
    }

    public async Task<ServiceResult<WalletGetByIdResponse>> GetWalletByIdAsync(Guid walletId, Guid userId)
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

    public async Task<ServiceResult<Guid>> CreateWalletAsync(Guid userId,WalletCreateRequest request)
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

    public async Task<ServiceResult> DeleteWalletAsync(Guid userId, Guid walletId)
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

    public async Task<ServiceResult> UpdateWalletAsync(Guid walletId,Guid userId, WalletUpdateRequest request)
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
        if (wallet.DailyTransferLimit > request.DailyTransferLimit)
        {
            return ServiceResult.Fail("New daily transfer limit cannot be lower than the current daily transfer limit");
        }
        if (!(request.DailyTransferLimit <= wallet.DailyTransferLimit * 3))
        {
            return ServiceResult.Fail("The daily transfer limit cannot be updated to more than three times the previous limit.");
        }

        wallet.DailyTransferLimit = request.DailyTransferLimit;
        walletRepository.Update(wallet);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }

    public async Task<ServiceResult> DepositAsync(Guid userId, Guid walletId, WalletDepositWithdrawalRequest depositRequest)
    {
        var wallet = await walletRepository.GetByIdAsync(walletId);
        if (wallet is null)
        {
            return ServiceResult.Fail("wallet not found");
        }
        if (wallet.UserId != userId)
        {
            return ServiceResult.Fail("User does not own the wallet");
        }
        if (!wallet.IsActive)
        {
            return ServiceResult.Fail("Wallet is not active");
        }
        if (!(depositRequest.Amount > 0))
        {
            return ServiceResult.Fail("amount can't be lower or equal to zero");
        }
        
        wallet.Balance += depositRequest.Amount;
        walletRepository.Update(wallet);
        var transaction = new Transaction()
        {
            Amount = depositRequest.Amount,
            BalanceAfter = wallet.Balance,
            WalletId = walletId,
            Description = depositRequest.Description,
            TransactionType = TransactionType.Deposit,
        };
        await transactionRepository.AddAsync(transaction);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }

    public async Task<ServiceResult> WithdrawalAsync(Guid userId, Guid walletId, WalletDepositWithdrawalRequest withdrawalRequest)
    {
        var wallet = await walletRepository.GetByIdAsync(walletId);
        if (wallet is null)
        {
            return ServiceResult.Fail("wallet not found");
        }
        if (wallet.UserId != userId)
        {
            return ServiceResult.Fail("User does not own the wallet");
        }
        if (!wallet.IsActive)
        {
            return ServiceResult.Fail("Wallet is not active");
        }

        if (!(withdrawalRequest.Amount > 0))
        {
            return ServiceResult.Fail("amount can't be lower or equal to zero");
        }
        if (withdrawalRequest.Amount > wallet.Balance)
        {
            return ServiceResult.Fail("amount withdraw cannot exceed the balance");
        }
        wallet.Balance -= withdrawalRequest.Amount;
        walletRepository.Update(wallet);
        var transaction = new Transaction()
        {
            Amount = withdrawalRequest.Amount,
            BalanceAfter = wallet.Balance,
            WalletId = walletId,
            Description = withdrawalRequest.Description,
            TransactionType = TransactionType.Withdrawal,
        };
        await transactionRepository.AddAsync(transaction);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }
}