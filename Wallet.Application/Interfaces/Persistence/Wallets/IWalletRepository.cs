using WalletEntity = Wallet.Domain.Entities.Wallet;

namespace Wallet.Application.Interfaces.Persistence.Wallets;

public interface IWalletRepository : IRepository<WalletEntity>
{
    Task<List<WalletEntity>> GetAllWalletsAsync(Guid userId);
    Task<WalletEntity?> GetWalletByUserId(Guid userId);
}
