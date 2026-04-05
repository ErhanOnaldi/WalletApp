using Wallet.Domain.Enums;

namespace Wallet.Domain.Entities;

public class Transaction : IAuditEntity
{
    public int Id { get; set; }
    public Guid WalletId { get; set; }
    public TransactionType TransactionType { get; set; }
    public Wallet Wallet { get; set; } = null!;
    public decimal Amount { get; set; }
    public decimal BalanceAfter { get; set; }
    public string? Description { get; set; }
    public Guid ReferenceId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}