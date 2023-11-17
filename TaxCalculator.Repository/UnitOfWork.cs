using TaxCalculator.Entities.Context;
using TaxCalculator.Repository.IRepositories;
using TaxCalculator.Repository.Repositories;

namespace TaxCalculator.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _dbContext;

        public UnitOfWork(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IPostalCodeTaxTypeRepository PostalCodeTaxTypeRepository =>
            new PostalCodeTaxTypeRepository(_dbContext);

        public IProgressiveTaxBracketRepository ProgressiveTaxBracketRepository =>
            new ProgressiveTaxBracketRepository(_dbContext);

        public ITaxCalculationRecordRepository TaxCalculationRecordRepository =>
            new TaxCalculationRecordRepository(_dbContext);

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
}
