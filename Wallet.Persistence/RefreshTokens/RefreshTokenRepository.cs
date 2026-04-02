using Wallet.Application.Interfaces.Persistence.RefreshTokens;
using Wallet.Domain.Entities;

namespace Wallet.Persistence.RefreshTokens;

public class RefreshTokenRepository(AppDbContext dbContext) : Repository<RefreshToken>(dbContext), IRefreshTokenRepository
{
}
