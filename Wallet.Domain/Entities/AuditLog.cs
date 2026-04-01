namespace Wallet.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public string Action { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public Guid EntityId { get; set; }
    public string? OldValues { get; set; }  // JSON string, nullable (oluşturma işlemlerinde önceki hal yok)
    public string? NewValues { get; set; }  // JSON string, nullable (silme işlemlerinde sonraki hal yok)
    public string? IpAddress { get; set; }
    public DateTime CreatedAt { get; set; }
}