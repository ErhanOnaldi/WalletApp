using Wallet.Application.Interfaces.Persistence.Transfers;

namespace Wallet.Persistence.Entities.Transfer;

public class TransferRepository(AppDbContext dbContext) : Repository<Domain.Entities.Transfer>(dbContext), ITransferRepository
{
}
