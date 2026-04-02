using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Wallet.Application.Features.Auth.DTOs;
using Wallet.Application.Interfaces.Token;
using Wallet.Domain.Options;

namespace Wallet.Token.Token;

public class TokenService(IOptions<TokenOption> options) : ITokenService
{
    public Task<string> GenerateJwtTokenAsync(AuthGenerateJwtTokenRequest request)
    {
        // Step 1 - Create signing key from your secret
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        // Step 2 - Build claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, request.Id.ToString()),
            new Claim(ClaimTypes.Email, request.Email),
            new Claim(ClaimTypes.Role, request.Role)
        };
        
        // Step 3 - Create the token
        var token = new JwtSecurityToken(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(options.Value.ExpiryMinutes),
            signingCredentials: credentials
        );

        // Step 4 - Serialize to string and return
        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));

    }

    public Task<string> GenerateRefreshTokenAsync()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Task.FromResult(Convert.ToBase64String(bytes));
    }
}