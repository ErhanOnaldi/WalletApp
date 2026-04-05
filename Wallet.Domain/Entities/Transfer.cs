using Wallet.Domain.Enums;

namespace Wallet.Domain.Entities;

public class Transfer : IAuditEntity
{
    public Guid Id { get; set; }
    public Guid ToWalletId { get; set; }
    public Wallet ToWallet { get; set; } = null!;
    public Guid FromWalletId { get; set; }
    public Wallet FromWallet { get; set; } = null!;
    public decimal Amount { get; set; }
    public decimal Fee { get; set; }
    public decimal ExchangeRate { get; set; }
    public TransferStatus TransferStatus { get; set; }
    public Guid IdempotencyKey { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CompletedAt { get; set; }

}