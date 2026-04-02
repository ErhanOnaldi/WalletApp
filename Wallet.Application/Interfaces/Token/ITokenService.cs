using Wallet.Application.Features.Auth.DTOs;

namespace Wallet.Application.Interfaces.Token;

public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(AuthGenerateJwtTokenRequest request);
    Task<string> GenerateRefreshTokenAsync();
    
}