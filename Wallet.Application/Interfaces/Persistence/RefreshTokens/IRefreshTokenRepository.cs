using Wallet.Domain.Entities;

namespace Wallet.Application.Interfaces.Persistence.RefreshTokens;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken);
}
