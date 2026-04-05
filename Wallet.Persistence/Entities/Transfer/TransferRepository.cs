using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces.Persistence.Transfers;
using Wallet.Domain.Enums;

namespace Wallet.Persistence.Entities.Transfer;

public class TransferRepository(AppDbContext dbContext) : Repository<Domain.Entities.Transfer>(dbContext), ITransferRepository
{
    public async Task<Domain.Entities.Transfer?> GetByIdempotencyKeyAsync(Guid idempotencyKey)
    {
        return await DbContext.Transfers.Where(x => x.IdempotencyKey == idempotencyKey).FirstOrDefaultAsync();
    }

    public async Task<decimal> GetTodaysTotalTransferredAmountAsync(Guid fromWalletId)
    {
        var sumAmount = await DbContext.Transfers.Where(x => x.FromWalletId == fromWalletId && x.CreatedAt >= DateTime.UtcNow.Date && x.TransferStatus == TransferStatus.Completed).SumAsync(x => x.Amount);
        return sumAmount;
    }

    public async Task<List<Domain.Entities.Transfer>> GetTransfersByWallet(Guid walletId)
    {
        var transfers = await DbContext.Transfers.Where(x => x.FromWalletId == walletId).ToListAsync();
        return transfers;
    }

    public Task<List<Domain.Entities.Transfer>> GetAllTransfersByUser(Guid userId)
    {
        var transfers = DbContext.Transfers.Where(x => x.FromWallet.UserId == userId).ToListAsync();
        return transfers;
    }
}
