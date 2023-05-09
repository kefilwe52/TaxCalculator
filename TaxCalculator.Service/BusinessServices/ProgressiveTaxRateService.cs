using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository;
using TaxCalculator.Service.BusinessContracts;

namespace TaxCalculator.Service.BusinessServices;

public class ProgressiveTaxRateService : IProgressiveTaxRateService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProgressiveTaxRateService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProgressiveTaxRate> GetProgressiveTaxRate(int postalCodeInfoId)
    {
        return await _unitOfWork.ProgressiveTaxRateRepository.FindOneAsync(x => x.PostalCodeInfoId == postalCodeInfoId);
    }
}