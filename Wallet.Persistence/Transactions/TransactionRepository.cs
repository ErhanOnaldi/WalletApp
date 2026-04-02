using Wallet.Application.Interfaces.Persistence.Transactions;
using Wallet.Domain.Entities;

namespace Wallet.Persistence.Transactions;

public class TransactionRepository(AppDbContext dbContext) : Repository<Transaction>(dbContext), ITransactionRepository
{
}
