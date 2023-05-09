using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository;
using TaxCalculator.Service.BusinessContracts;

namespace TaxCalculator.Service.BusinessServices;

public class PostalCodeInfoService : IPostalCodeInfoService
{
    private readonly IUnitOfWork _unitOfWork;

    public PostalCodeInfoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PostalCodeInfo> GetPostalCodeInfoAsync(string postalCode)
    {
        return await _unitOfWork.PostalCodeInfoRepository.FindOneAsync(x =>
            string.Equals(x.Code, postalCode, StringComparison.CurrentCultureIgnoreCase));
    }

    public async Task<IEnumerable<PostalCodeInfo>> GetAllPostalCodeInfoAsync()
    {
        return await _unitOfWork.PostalCodeInfoRepository.GetAllAsync();
    }
}