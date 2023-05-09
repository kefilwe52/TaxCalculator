using Microsoft.EntityFrameworkCore;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository.IRepositories;

namespace TaxCalculator.Repository.Repositories;

public class TaxCalculationRepository : GenericRepository<TaxCalculation>, ITaxCalculationRepository
{
    public TaxCalculationRepository(DbContext context) : base(context)
    {
    }
}