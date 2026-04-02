using Wallet.Application.Interfaces.Persistence.Transfers;
using Wallet.Domain.Entities;

namespace Wallet.Persistence.Transfers;

public class TransferRepository(AppDbContext dbContext) : Repository<Transfer>(dbContext), ITransferRepository
{
}
