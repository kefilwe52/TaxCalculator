using TaxCalculator.Entities.Context;
using TaxCalculator.Repository.IRepositories;
using TaxCalculator.Repository.Repositories;

namespace TaxCalculator.Repository;

public class UnitOfWork : IUnitOfWork
{
    private DataContext _dbContext;

    public UnitOfWork(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IPostalCodeInfoRepository PostalCodeInfoRepository => new PostalCodeInfoRepository(_dbContext);
    public IProgressiveTaxRateRepository ProgressiveTaxRateRepository => new ProgressiveTaxRateRepository(_dbContext);
    public ITaxCalculationRepository TaxCalculationRepository => new TaxCalculationRepository(_dbContext);

    public int Commit()
    {
        return _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
            if (_dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
    }
}