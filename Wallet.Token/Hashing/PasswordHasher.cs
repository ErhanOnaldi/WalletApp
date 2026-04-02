using Microsoft.AspNetCore.Identity;
using Wallet.Application.Interfaces.Token.Hashing;
using Wallet.Domain.Entities;

namespace Wallet.Token.Hashing;

public class AppPasswordHasher() : IPasswordHasher
{
    private readonly PasswordHasher<User?> _hasher = new PasswordHasher<User?>();
    public string Hash(string password)
    {
        return _hasher.HashPassword(null,password);
    }

    public bool Verify(string hashedPassword, string providedPassword)
    {
        var result =_hasher.VerifyHashedPassword(null,hashedPassword, providedPassword);
        return result != PasswordVerificationResult.Failed;
    }
}