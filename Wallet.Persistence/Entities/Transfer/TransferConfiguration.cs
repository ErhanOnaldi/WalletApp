using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wallet.Persistence.Entities.Transfer;

public class TransferConfiguration : IEntityTypeConfiguration<Domain.Entities.Transfer>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Transfer> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Amount)
            .IsRequired()
            .HasColumnType("decimal(18, 4)");
            
        builder.Property(c => c.Fee)
            .IsRequired()
            .HasColumnType("decimal(18, 4)");
            
        builder.Property(c => c.ExchangeRate)
            .IsRequired()
            .HasColumnType("decimal(18, 6)");
            
        builder.Property(c => c.TransferStatus)
            .IsRequired();
            
        builder.Property(c => c.IdempotencyKey)
            .IsRequired();
            
        builder.Property(c => c.CreatedAt)
            .IsRequired();
            
        builder.Property(c => c.CompletedAt)
            .IsRequired();

        // Need Restrict to avoid multiple cascade paths in EF Core
        builder.HasOne(t => t.FromWallet)
            .WithMany()
            .HasForeignKey(t => t.FromWalletId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.ToWallet)
            .WithMany()
            .HasForeignKey(t => t.ToWalletId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
