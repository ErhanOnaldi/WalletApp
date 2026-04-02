using Microsoft.Extensions.DependencyInjection;
using Wallet.Application.Interfaces.Token;
using Wallet.Application.Interfaces.Token.Hashing;
using Wallet.Token.Hashing;
using Wallet.Token.Token;

namespace Wallet.Token.Extensions;

public static class TokenExtensions
{
    public static IServiceCollection AddToken(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher,AppPasswordHasher>();
        services.AddScoped<ITokenService,TokenService>();
        return services;
    }
}