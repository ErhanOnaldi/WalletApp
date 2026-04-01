using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Wallet.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        //Tüm entity configlerini tek bir çatı altında geçen o kod, bu assembly içinde çalışan tüm EntityConfiguration patternine sahip classları alır
        // ve işler
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}