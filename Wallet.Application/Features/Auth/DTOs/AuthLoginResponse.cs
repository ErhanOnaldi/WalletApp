namespace Wallet.Application.Features.Auth.DTOs;

public record AuthLoginResponse(string AccessToken, string RefreshToken, DateTime AccessTokenExpiresAt);