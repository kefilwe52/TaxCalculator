using TaxCalculator.Repository.IRepositories;

namespace TaxCalculator.Repository;

public interface IUnitOfWork : IDisposable
{
    IPostalCodeInfoRepository PostalCodeInfoRepository { get; }
    IProgressiveTaxRateRepository ProgressiveTaxRateRepository { get; }
    ITaxCalculationRepository TaxCalculationRepository { get; }
    int Commit();
}