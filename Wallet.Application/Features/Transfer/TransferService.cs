using Wallet.Application.Features.ExchangeRate;
using Wallet.Application.Features.ExchangeRate.DTOs;
using Wallet.Application.Features.Transfer.DTOs;
using Wallet.Application.Interfaces.Persistence;
using Wallet.Application.Interfaces.Persistence.Transactions;
using Wallet.Application.Interfaces.Persistence.Transfers;
using Wallet.Application.Interfaces.Persistence.Wallets;
using Wallet.Domain.Entities;
using Wallet.Domain.Enums;

namespace Wallet.Application.Features.Transfer;

public class TransferService(ITransferRepository transferRepository, 
    ITransactionRepository transactionRepository, 
    IWalletRepository walletRepository, 
    IExchangeRateService exchangeRateService, IUnitOfWork unitOfWork) : ITransferService
{
    public async Task<ServiceResult> TransferAsync(Guid userId,TransferRequest request)
    {
        var transferCheck = await transferRepository.GetByIdempotencyKeyAsync(request.IdempotencyKey);
        if (transferCheck is not null)
        {
            return ServiceResult.Success();
        }
        
        var fromWallet = await walletRepository.GetByIdAsync(request.FromWalletId);
        var toWallet = await walletRepository.GetByIdAsync(request.ToWalletId);
        if (fromWallet is null)
        {
            return ServiceResult.Fail("fromWallet value is null");
        }
        if (toWallet is null)
        {
            return ServiceResult.Fail("toWallet value is null");
        }

        if (!(fromWallet.IsActive && toWallet.IsActive))
        {
            return ServiceResult.Fail("fromWallet or toWallet is not active");
        }
        if (fromWallet.UserId != userId)
        {
            return ServiceResult.Fail("UserId does not match with fromWallet");
        }

        if (!(request.Amount > 0))
        {
            return ServiceResult.Fail("amount must be greater than 0");
        }

        if (request.Amount > fromWallet.Balance)
        {
            return ServiceResult.Fail("amount must be less than balance");
        }

        var totalTransferredAmountAsync = await transferRepository.GetTodaysTotalTransferredAmountAsync(fromWallet.UserId);
        if (totalTransferredAmountAsync + request.Amount > fromWallet.DailyTransferLimit)
        {
            return ServiceResult.Fail("amount exceeds daily transfer limit");
        }
        
        var exchangeRate = await exchangeRateService.GetExchangeRateAsync(new ExchangeRateRequest(fromWallet.Currency, toWallet.Currency, request.Amount));
        if (exchangeRate is null)
        {
            return ServiceResult.Fail("exchangeRate value is null");
        }
        
        fromWallet.Balance -= request.Amount;
        toWallet.Balance += request.Amount * exchangeRate.Value;
        walletRepository.Update(fromWallet);
        walletRepository.Update(toWallet);
        var transfer = new Domain.Entities.Transfer
        {
            Id = Guid.NewGuid(),
            IdempotencyKey = request.IdempotencyKey,
            FromWalletId = fromWallet.Id,
            ToWalletId = toWallet.Id,
            Amount = request.Amount,
            Fee = 0,
            ExchangeRate =  exchangeRate.Value,
            TransferStatus = TransferStatus.Completed,
            CompletedAt = DateTime.UtcNow,
        };
        await transferRepository.AddAsync(transfer);
        await transactionRepository.AddAsync(
            new Transaction()
            {
                Amount = request.Amount,
                BalanceAfter = fromWallet.Balance,
                WalletId = fromWallet.Id,
                Description = request.Description,
                TransactionType = TransactionType.TransferOut,
                ReferenceId = transfer.Id
            }
        );
        await transactionRepository.AddAsync(
            new Transaction()
            {
                Amount = request.Amount,
                BalanceAfter = toWallet.Balance,
                WalletId = toWallet.Id,
                Description = request.Description,
                TransactionType = TransactionType.TransferIn,
                ReferenceId = transfer.Id
            }
        );
        await unitOfWork.SaveChangesAsync();
        return  ServiceResult.Success();
    }
    public async Task<ServiceResult<TransferGetResponse>> GetTransferById(Guid transferId,Guid userId)
    {
        var transfer = await transferRepository.GetByIdAsync(transferId);
        if (transfer is null)
        {
            return ServiceResult<TransferGetResponse>.Fail("transfer not found");
        }
        var wallet = await walletRepository.GetByIdAsync(userId);
        if (transfer.FromWalletId !=  wallet?.Id )
        {
            return ServiceResult<TransferGetResponse>.Fail("transfer does not belong to userId");
        }
        var transferAsDto = new TransferGetResponse(transfer.IdempotencyKey,
            transfer.ToWalletId,
            transfer.FromWalletId,
            transfer.TransferStatus,
            transfer.CreatedAt,
            transfer.CompletedAt,
            transfer.Amount,
            transfer.Fee,
            transfer.ExchangeRate);
        return ServiceResult<TransferGetResponse>.Success(transferAsDto);
    }
    public async Task<ServiceResult<List<TransferGetResponse>>> GetTransfersByUser(Guid userId)
    {
        var transfers =await transferRepository.GetAllTransfersByUser(userId);
        var transfersDto = transfers.Select(transfer => new TransferGetResponse(transfer.IdempotencyKey,
            transfer.ToWalletId,
            transfer.FromWalletId,
            transfer.TransferStatus,
            transfer.CreatedAt,
            transfer.CompletedAt,
            transfer.Amount,
            transfer.Fee,
            transfer.ExchangeRate)).ToList();
        return ServiceResult<List<TransferGetResponse>>.Success(transfersDto);
    }
}