using System.Linq.Expressions;
using TaxCalculator.Entities;

namespace TaxCalculator.Repository;

public interface IGenericRepository<T> where T : class, IEntity<int>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);

    Task<T> FindOneAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    T Delete(int id);
    void Edit(T entity);
}