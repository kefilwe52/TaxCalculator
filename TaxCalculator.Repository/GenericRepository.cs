using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Entities;

namespace TaxCalculator.Repository;

public abstract class GenericRepository<U> : IGenericRepository<U>
    where U : class, IEntity<int>
{
    protected readonly DbSet<U> Dbset;
    protected readonly DbContext Entities;

    protected GenericRepository(DbContext context)
    {
        Entities = context;
        Dbset = context.Set<U>();
    }

    public virtual async Task<IEnumerable<U>> GetAllAsync()
    {
        var results = await Dbset.ToListAsync();

        return results;
    }

    public async Task<IEnumerable<U>> FindByAsync(Expression<Func<U, bool>> predicate)
    {
        var query = Dbset.Where(predicate);

        return await query.ToListAsync();
    }

    public async Task<U> FindOneAsync(Expression<Func<U, bool>> predicate)
    {
        return await Dbset.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<U> AddAsync(U entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        var model = await Dbset.AddAsync(entity);

        return model.Entity;
    }

    public U Delete(int id)
    {
        var entity = Dbset.Find(id);
        if (entity != null)
        {
            var model = Dbset.Remove(entity);
            return model.Entity;
        }

        return null;
    }

    public virtual void Edit(U entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        Entities.Entry(entity).State = EntityState.Modified;
    }
}