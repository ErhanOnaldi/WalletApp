using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces.Persistence.RefreshTokens;

namespace Wallet.Persistence.Entities.RefreshToken;

public class RefreshTokenRepository(AppDbContext dbContext) : Repository<Domain.Entities.RefreshToken>(dbContext), IRefreshTokenRepository
{
    public Task<Domain.Entities.RefreshToken?> GetByRefreshTokenAsync(string refreshToken)
    {
        return DbContext.RefreshTokens.Where(x => x.Token == refreshToken).Include(x => x.User).FirstOrDefaultAsync();
    }
}
