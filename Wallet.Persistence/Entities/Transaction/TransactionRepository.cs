using Wallet.Application.Interfaces.Persistence.Transactions;

namespace Wallet.Persistence.Entities.Transaction;

public class TransactionRepository(AppDbContext dbContext) : Repository<Domain.Entities.Transaction>(dbContext), ITransactionRepository
{
}
