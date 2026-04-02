using Wallet.Domain.Entities;

namespace Wallet.Application.Interfaces.Persistence.AuditLogs;

public interface IAuditLogRepository : IRepository<AuditLog>
{
}
