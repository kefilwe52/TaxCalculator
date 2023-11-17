using Microsoft.EntityFrameworkCore;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository.IRepositories;

namespace TaxCalculator.Repository.Repositories
{
    public class ProgressiveTaxBracketRepository : GenericRepository<ProgressiveTaxBracket>,
        IProgressiveTaxBracketRepository
    {
        public ProgressiveTaxBracketRepository(DbContext context) : base(context)
        {
        }
    }
}
