using Wallet.Application.Interfaces.Persistence;

namespace Wallet.Persistence;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    // kaç satır etkilendi onu döner
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}