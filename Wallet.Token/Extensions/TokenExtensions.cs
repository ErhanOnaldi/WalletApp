using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Wallet.Application.Interfaces.Token;
using Wallet.Application.Interfaces.Token.Hashing;
using Wallet.Domain.Options;
using Wallet.Token.Hashing;
using Wallet.Token.Token;

namespace Wallet.Token.Extensions;

public static class TokenExtensions
{
    public static IServiceCollection AddToken(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenOption>(configuration.GetSection(nameof(TokenOption)));

        var tokenOptions = configuration.GetSection(nameof(TokenOption)).Get<TokenOption>()
                           ?? throw new InvalidOperationException("TokenOption configuration is missing.");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = tokenOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = tokenOptions.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(tokenOptions.SecretKey)),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();

        services.AddScoped<IPasswordHasher, AppPasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}