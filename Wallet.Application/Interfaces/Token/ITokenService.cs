using Wallet.Application.Features.Auth.DTOs;

namespace Wallet.Application.Interfaces.Token;

public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(AuthLogoutRequest request);
    Task<string> GenerateRefreshTokenAsync();
    
}