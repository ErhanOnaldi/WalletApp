using System.Linq.Expressions;

namespace Wallet.Application.Interfaces.Persistence;

public interface IRepository<T> where T : class
{
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetPagedAsync(int pageNumber, int pageSize);
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    ValueTask<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}