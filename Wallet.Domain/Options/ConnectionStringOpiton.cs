namespace Wallet.Domain.Options;

public class ConnectionStringOpiton
{
    public const string Key = "ConnectionStrings";
    public string Default { get; set; } = null!;
}