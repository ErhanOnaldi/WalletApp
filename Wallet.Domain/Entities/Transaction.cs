namespace Wallet.Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int WalletId { get; set; }
    public Wallet Wallet { get; set; }
    public decimal Amount { get; set; }
    public decimal BalanceAfter { get; set; }
    public string? Description { get; set; }
    public Guid ReferenceId { get; set; }
    public DateTime CreatedAt { get; set; }
}