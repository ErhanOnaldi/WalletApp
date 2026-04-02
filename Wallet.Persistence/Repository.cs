using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces.Persistence;

namespace Wallet.Persistence;

public class Repository<T>(AppDbContext dbContext) : IRepository<T> where T : class
{
    protected AppDbContext DbContext { get; } = dbContext;
    protected DbSet<T> DbSet { get; } = dbContext.Set<T>();
    
    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => DbSet.AnyAsync(predicate);
    public Task<List<T>> GetAllAsync() => DbSet.ToListAsync();
    public Task<List<T>> GetPagedAsync(int pageNumber, int pageSize) => DbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => DbSet.Where(predicate).AsQueryable().AsNoTracking();
    public ValueTask<T?> GetByIdAsync(Guid id) => DbSet.FindAsync(id);
    public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);
    public void Update(T entity) => DbSet.Update(entity);
    public void Delete(T entity) => DbSet.Remove(entity);
}
