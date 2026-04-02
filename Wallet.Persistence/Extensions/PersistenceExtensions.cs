using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Application.Interfaces.Persistence.AuditLogs;
using Wallet.Application.Interfaces.Persistence.ExchangeRates;
using Wallet.Application.Interfaces.Persistence.RefreshTokens;
using Wallet.Application.Interfaces.Persistence.Transactions;
using Wallet.Application.Interfaces.Persistence.Transfers;
using Wallet.Application.Interfaces.Persistence.Users;
using Wallet.Application.Interfaces.Persistence.Wallets;
using Wallet.Domain.Options;
using Wallet.Persistence.Entities.AuditLog;
using Wallet.Persistence.Entities.ExchangeRate;
using Wallet.Persistence.Entities.RefreshToken;
using Wallet.Persistence.Entities.Transaction;
using Wallet.Persistence.Entities.Transfer;
using Wallet.Persistence.Entities.User;
using Wallet.Persistence.Entities.Wallet;
using Wallet.Persistence.Interceptors;

namespace Wallet.Persistence.Extensions;

public static class PersistenceExtensions // Extension Classlar static olmalı
{
    //Service Provider DI container'ın kendisidir
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AuditDbContextInterceptor>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
        services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITransferRepository, TransferRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();
    
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetSection(ConnectionStringOpiton.Key).Get<ConnectionStringOpiton>();
            options.UseNpgsql(connectionString!.Default, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
            });
            options.AddInterceptors(serviceProvider.GetRequiredService<AuditDbContextInterceptor>());
        });
        return services;
    }
    
}
