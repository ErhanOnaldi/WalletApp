using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Domain.Options;
using Wallet.Persistence.Interceptors;

namespace Wallet.Persistence.Extensions;

public static class PersistenceExtensions // Extension Classlar static olmalı
{
    //Service Provider DI container'ın kendisidir
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AuditDbContextInterceptor>();
    
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