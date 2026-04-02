namespace Wallet.Application.Features.Auth.DTOs;

public record AuthRegisterRequest(string Email, string Password, string FullName);