using Wallet.Domain.Enums;

namespace Wallet.Domain.Entities;

public class Wallet
{
    public int Id { get; set; }
    public User User { get; set; } = null!;
    public List<Transaction>? Transactions { get; set; }
    public Guid UserId { get; set; }
    public Currency Currency { get; set; }
    public decimal Balance { get; set; }
    public decimal DailyTransferLimit { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}