using Wallet.Application.Interfaces.Persistence.AuditLogs;
using Wallet.Domain.Entities;

namespace Wallet.Persistence.AuditLogs;

public class AuditLogRepository(AppDbContext dbContext) : Repository<AuditLog>(dbContext), IAuditLogRepository
{
}
