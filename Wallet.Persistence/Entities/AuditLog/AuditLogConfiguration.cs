using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wallet.Persistence.Entities.AuditLog;

public class AuditLogConfiguration : IEntityTypeConfiguration<Domain.Entities.AuditLog>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.AuditLog> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Action)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(c => c.EntityType)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(c => c.EntityId)
            .IsRequired();
            
        builder.Property(c => c.OldValues)
            .IsRequired(false);
            
        builder.Property(c => c.NewValues)
            .IsRequired(false);
            
        builder.Property(c => c.IpAddress)
            .IsRequired(false)
            .HasMaxLength(50);
            
        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.HasOne(c => c.User)
            .WithMany(u => u.AuditLogs)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
