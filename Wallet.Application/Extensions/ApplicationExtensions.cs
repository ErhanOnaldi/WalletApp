using Microsoft.Extensions.DependencyInjection;
using Wallet.Application.Features.Auth;
using Wallet.Application.Interfaces.Token.Hashing;

namespace Wallet.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplications(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        
        return services;
    }
}