using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
namespace Wallet.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.User> Users { get; set; } = default!;
    public DbSet<Domain.Entities.Wallet> Wallets { get; set; } = default!;
    public DbSet<Domain.Entities.Transaction> Transactions { get; set; } = default!;
    public DbSet<Domain.Entities.Transfer> Transfers { get; set; } = default!;
    public DbSet<Domain.Entities.ExchangeRate> ExchangeRates { get; set; } = default!;
    public DbSet<Domain.Entities.AuditLog> AuditLogs { get; set; } = default!;
    public DbSet<Domain.Entities.RefreshToken> RefreshTokens { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        //Tüm entity configlerini tek bir çatı altında geçen o kod, bu assembly içinde çalışan tüm EntityConfiguration patternine sahip classları alır
        // ve işler
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}