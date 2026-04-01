using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Wallet.Domain.Entities;

namespace Wallet.Persistence.Interceptors;

public class AuditDbContextInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
        {
            if(entityEntry.Entity is not IAuditEntity entity) continue;

            switch (entityEntry.State)
            {
                case(EntityState.Added):
                    entity.CreatedAt = DateTime.UtcNow;
                    entityEntry.Context.Entry(entity).Property(x => x.UpdatedAt).IsModified = false;
                    break;
                case(EntityState.Modified):
                    entity.UpdatedAt = DateTime.UtcNow;
                    entityEntry.Context.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}