using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Service.BusinessContracts;

public interface IProgressiveTaxRateService
{
    Task<ProgressiveTaxRate> GetProgressiveTaxRate(int postalCodeInfoId);
}