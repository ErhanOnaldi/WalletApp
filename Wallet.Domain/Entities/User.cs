namespace Wallet.Domain.Entities;

public class User : IAuditEntity
{
    public Guid Id { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public List<Wallet> Wallets { get; set; } = new List<Wallet>();
    public List<AuditLog>? AuditLogs { get; set; } = new List<AuditLog>();
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}