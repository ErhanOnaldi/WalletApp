namespace Wallet.Application.Features.Auth.DTOs;

public record AuthGenerateJwtTokenRequest(Guid Id, string Email, string Role = "User");