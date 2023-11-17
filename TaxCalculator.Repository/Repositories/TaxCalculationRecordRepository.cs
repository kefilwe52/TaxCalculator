using Microsoft.EntityFrameworkCore;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository.IRepositories;

namespace TaxCalculator.Repository.Repositories
{
    public class TaxCalculationRecordRepository : GenericRepository<TaxCalculationRecord>, ITaxCalculationRecordRepository
    {
        public TaxCalculationRecordRepository(DbContext context) : base(context)
        {
        }
    }
}
