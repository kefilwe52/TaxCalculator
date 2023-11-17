using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository;
using TaxCalculator.Service.BusinessContracts;

namespace TaxCalculator.Service.BusinessServices
{
    public class PostalCodeTaxTypeService : IPostalCodeTaxTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostalCodeTaxTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PostalCodeTaxType> GetPostalCodeTaxType(int postalCodeId)
        {
            return await _unitOfWork.PostalCodeTaxTypeRepository.FindByAsync(x => x.Id == postalCodeId);
        }

        public async Task<IEnumerable<PostalCodeTaxType>> PostalCodeTaxTypes()
        {
            return await _unitOfWork.PostalCodeTaxTypeRepository.GetAllAsync();
        }
    }
}
