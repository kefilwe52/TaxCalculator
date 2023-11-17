using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Service.BusinessContracts
{
    public interface ITaxCalculationRecordService
    {
        Task<TaxCalculationRecord> AddTaxCalculationRecord(TaxCalculationRecord taxCalculationRecord);

        Task<TaxCalculationRecord> GetTaxCalculationRecord(int taxCalculationRecordId);

        Task<IEnumerable<TaxCalculationRecord>> GetTaxCalculationRecords();
    }
}
