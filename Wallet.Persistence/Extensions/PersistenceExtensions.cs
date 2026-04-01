using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Domain.Options;

namespace Wallet.Persistence.Extensions;

public static class PersistenceExtensions // Extension Classlar static olmalı
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetSection(ConnectionStringOpiton.Key).Get<ConnectionStringOpiton>();
            options.UseNpgsql(connectionString!.Default, sqlOptions =>
            {
                //Migration'ın Repository kullanılması için
                sqlOptions.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
            });
            options.UseNpgsql();
        });
        return services;
    }
    
}