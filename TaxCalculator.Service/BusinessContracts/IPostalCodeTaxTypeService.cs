using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Service.BusinessContracts
{
    public interface IPostalCodeTaxTypeService
    {
        Task<PostalCodeTaxType> GetPostalCodeTaxType(int postalCodeId);

        Task<IEnumerable<PostalCodeTaxType>> PostalCodeTaxTypes();
    }
}
