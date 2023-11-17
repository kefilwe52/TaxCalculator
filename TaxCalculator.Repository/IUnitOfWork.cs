using TaxCalculator.Repository.IRepositories;

namespace TaxCalculator.Repository
{
    public interface IUnitOfWork
    {
        IPostalCodeTaxTypeRepository PostalCodeTaxTypeRepository { get; }
        IProgressiveTaxBracketRepository ProgressiveTaxBracketRepository { get; }
        ITaxCalculationRecordRepository TaxCalculationRecordRepository { get; }
        int Commit();
    }
}
