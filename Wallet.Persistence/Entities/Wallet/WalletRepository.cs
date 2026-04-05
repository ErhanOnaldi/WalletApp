using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces.Persistence.Wallets;
using WalletEntity = Wallet.Domain.Entities.Wallet;

namespace Wallet.Persistence.Entities.Wallet;

public class WalletRepository(AppDbContext dbContext) : Repository<WalletEntity>(dbContext), IWalletRepository
{
    public Task<List<WalletEntity>> GetAllWalletsAsync(Guid userId)
    {
        return DbContext.Wallets.Where(x => x.UserId == userId).ToListAsync();
    }
}
