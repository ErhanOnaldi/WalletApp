using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Wallet.Persistence.Entities.User;

public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.FullName)
            .IsRequired().
            HasMaxLength(50);
        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(c => c.PasswordHash)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(c => c.IsActive)
            .IsRequired();
        builder.Property(c => c.CreatedAt)
            .IsRequired();
    }
}