using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository;
using TaxCalculator.Service.BusinessContracts;
using TaxCalculator.Service.Calculations;

namespace TaxCalculator.Service.BusinessServices;

public class TaxCalculationService : ITaxCalculationService
{
    private readonly IUnitOfWork _unitOfWork;

    public TaxCalculationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<TaxCalculation> CreateTaxCalculationAsync(TaxCalculation taxCalculation)
    {
        ITaxCalculator taxCalculator;

        if (taxCalculation == null)
            throw new ArgumentNullException(nameof(taxCalculation));

        var existCalculations = await GetTaxCalculationAsync(taxCalculation.PostalCodeInfoId, taxCalculation.AnnualIncome);

        if (existCalculations != null)
            return existCalculations;

        switch ((await _unitOfWork.PostalCodeInfoRepository.FindOneAsync(x => x.Id == taxCalculation.PostalCodeInfoId))
                ?.CalculationType)
        {
            case "Progressive":
                var taxRates = await _unitOfWork.ProgressiveTaxRateRepository
                    .FindByAsync(x => x.PostalCodeInfoId == taxCalculation.PostalCodeInfoId);
                taxCalculator = new ProgressiveTaxCalculator(taxRates);
                break;
            case "FlatRate":
                var flatRate =
                    await _unitOfWork.PostalCodeInfoRepository.FindByAsync(x =>
                        x.Id == taxCalculation.PostalCodeInfoId);
                taxCalculator = new FlatRateTaxCalculator(flatRate.FirstOrDefault()?.FlatRateTax ?? 0);
                break;
            case "FlatValue":
                var valueRate =
                    await _unitOfWork.PostalCodeInfoRepository.FindByAsync(x =>
                        x.Id == taxCalculation.PostalCodeInfoId);
                taxCalculator = new FlatValueTaxCalculator(valueRate.FirstOrDefault()?.FlatValueTax ?? 0);
                break;
            default:
                throw new NotImplementedException();
        }

        var tax = taxCalculator.CalculateTax(taxCalculation.AnnualIncome);

        taxCalculation.TaxAmount = tax;
        taxCalculation.CreatedDateTime = DateTime.Now;

        var result = await _unitOfWork.TaxCalculationRepository.AddAsync(taxCalculation);

        _unitOfWork.Commit();

        return result;
    }

    public async Task<TaxCalculation> GetTaxCalculationAsync(int taxCalculationId)
    {
        return await _unitOfWork.TaxCalculationRepository.FindOneAsync(x => x.Id == taxCalculationId);
    }

    public async Task<IEnumerable<TaxCalculation>> GetTaxCalculationsAsync()
    {
        return await _unitOfWork.TaxCalculationRepository.GetAllAsync();
    }

    private async Task<TaxCalculation> GetTaxCalculationAsync(int postalCodeId, decimal annualIncome)
    {
        var taxCalculations = await _unitOfWork
            .TaxCalculationRepository
            .FindByAsync(x => x.PostalCodeInfoId == postalCodeId && x.AnnualIncome == annualIncome);

        return taxCalculations.FirstOrDefault();
    }
}