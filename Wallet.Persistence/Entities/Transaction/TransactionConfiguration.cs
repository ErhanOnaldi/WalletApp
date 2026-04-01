using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wallet.Persistence.Entities.Transaction;

public class TransactionConfiguration : IEntityTypeConfiguration<Domain.Entities.Transaction>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Transaction> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Amount)
            .IsRequired()
            .HasColumnType("decimal(18, 4)");
            
        builder.Property(c => c.BalanceAfter)
            .IsRequired()
            .HasColumnType("decimal(18, 4)");
            
        builder.Property(c => c.Description)
            .IsRequired(false)
            .HasMaxLength(500);
            
        builder.Property(c => c.ReferenceId)
            .IsRequired();
            
        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.HasOne(c => c.Wallet)
            .WithMany(w => w.Transactions)
            .HasForeignKey(c => c.WalletId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
