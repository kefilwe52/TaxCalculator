using Microsoft.EntityFrameworkCore;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository.IRepositories;

namespace TaxCalculator.Repository.Repositories;

public class ProgressiveTaxRateRepository : GenericRepository<ProgressiveTaxRate>, IProgressiveTaxRateRepository
{
    public ProgressiveTaxRateRepository(DbContext context) : base(context)
    {
    }
}