using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Service.BusinessContracts;

public interface ITaxCalculationService
{
    Task<TaxCalculation> CreateTaxCalculationAsync(TaxCalculation taxCalculation);

    Task<TaxCalculation> GetTaxCalculationAsync(int taxCalculationId);

    Task<IEnumerable<TaxCalculation>> GetTaxCalculationsAsync();
}