using System.Linq.Expressions;
using TaxCalculator.Entities;

namespace TaxCalculator.Repository;

public interface IGenericRepository<T> where T : class, IEntity<int>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> FindByAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}