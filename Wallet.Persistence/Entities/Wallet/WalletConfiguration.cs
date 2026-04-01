using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wallet.Persistence.Entities.Wallet;

public class WalletConfiguration : IEntityTypeConfiguration<Domain.Entities.Wallet>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Wallet> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Currency)
            .IsRequired();
            
        builder.Property(c => c.Balance)
            .IsRequired()
            .HasColumnType("decimal(18, 4)");
            
        builder.Property(c => c.DailyTransferLimit)
            .IsRequired()
            .HasColumnType("decimal(18, 4)");
            
        builder.Property(c => c.CreatedAt)
            .IsRequired();
            
        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.HasOne(w => w.User)
            .WithMany(u => u.Wallets)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
