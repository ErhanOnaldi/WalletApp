using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces.Persistence;

namespace Wallet.Persistence;

public class Repository<T>(AppDbContext dbContext) : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();
    
    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => _dbSet.AnyAsync(predicate);
    public Task<List<T>> GetAllAsync() => _dbSet.ToListAsync();
    public Task<List<T>> GetPagedAsync(int pageNumber, int pageSize) => _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsQueryable().AsNoTracking();
    public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Update(T entity) => _dbSet.Update(entity);
    public void Delete(T entity) => _dbSet.Remove(entity);
}