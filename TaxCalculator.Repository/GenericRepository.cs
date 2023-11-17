using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaxCalculator.Entities;

namespace TaxCalculator.Repository
{
    public abstract class GenericRepository<U> : IGenericRepository<U>
    where U : class, IEntity<int>
    {
        protected readonly DbSet<U> _dbSet;
        protected readonly DbContext _context;

        protected GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<U>();
        }

        public async Task AddAsync(U entity) => await _dbSet.AddAsync(entity);

        public void Delete(U entity) => _dbSet.Remove(entity);

        public async Task<U> FindByAsync(Expression<Func<U, bool>> predicate)
        => await _dbSet.Where(predicate).AsNoTracking().FirstOrDefaultAsync();

        public async Task<IEnumerable<U>> GetAllAsync() => await _dbSet.ToListAsync();

        public void Update(U entity) => _dbSet.Update(entity);
    }
}
