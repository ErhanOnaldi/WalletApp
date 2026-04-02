namespace Wallet.Application.Interfaces.Token.Hashing;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string hashedPassword, string providedPassword);
}