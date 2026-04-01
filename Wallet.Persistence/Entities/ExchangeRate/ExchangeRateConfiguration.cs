using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wallet.Persistence.Entities.ExchangeRate;

public class ExchangeRateConfiguration : IEntityTypeConfiguration<Domain.Entities.ExchangeRate>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.ExchangeRate> builder)
    {
        builder.HasKey("Id"); 
        
        builder.Property(c => c.ToCurrency)
            .IsRequired();
            
        builder.Property(c => c.FromCurrency)
            .IsRequired();
            
        builder.Property(c => c.Rate)
            .IsRequired()
            .HasColumnType("decimal(18, 6)"); // Standard for exchange rates
            
        builder.Property(c => c.LastUpdated)
            .IsRequired();
    }
}
