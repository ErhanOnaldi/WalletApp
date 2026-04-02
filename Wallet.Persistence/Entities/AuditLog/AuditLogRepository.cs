using Wallet.Application.Interfaces.Persistence.AuditLogs;

namespace Wallet.Persistence.Entities.AuditLog;

public class AuditLogRepository(AppDbContext dbContext) : Repository<Domain.Entities.AuditLog>(dbContext), IAuditLogRepository
{
}
