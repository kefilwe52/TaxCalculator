using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Service.BusinessContracts;

public interface IPostalCodeInfoService
{
    Task<PostalCodeInfo> GetPostalCodeInfoAsync(string postalCode);

    Task<IEnumerable<PostalCodeInfo>> GetAllPostalCodeInfoAsync();
}