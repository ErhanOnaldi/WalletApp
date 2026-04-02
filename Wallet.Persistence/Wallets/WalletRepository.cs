using Wallet.Application.Interfaces.Persistence.Wallets;
using WalletEntity = Wallet.Domain.Entities.Wallet;

namespace Wallet.Persistence.Wallets;

public class WalletRepository(AppDbContext dbContext) : Repository<WalletEntity>(dbContext), IWalletRepository
{
}
