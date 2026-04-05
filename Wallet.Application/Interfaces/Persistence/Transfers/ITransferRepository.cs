using Wallet.Domain.Entities;

namespace Wallet.Application.Interfaces.Persistence.Transfers;

public interface ITransferRepository : IRepository<Transfer>
{
    Task<Transfer?> GetByIdempotencyKeyAsync(Guid idempotencyKey);
    Task<decimal> GetTodaysTotalTransferredAmountAsync(Guid fromWalletId);
    Task<List<Transfer>> GetTransfersByWallet(Guid walletId);
}
