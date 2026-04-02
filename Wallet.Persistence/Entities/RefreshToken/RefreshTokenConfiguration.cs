using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wallet.Persistence.Entities.RefreshToken;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<Domain.Entities.RefreshToken>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.RefreshToken> builder)
    {
        builder.HasKey(c => c.Id);
    }
}