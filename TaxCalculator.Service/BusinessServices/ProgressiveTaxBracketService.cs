using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository;
using TaxCalculator.Service.BusinessContracts;

namespace TaxCalculator.Service.BusinessServices
{
    public class ProgressiveTaxBracketService : IProgressiveTaxBracketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgressiveTaxBracketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProgressiveTaxBracket>> GetProgressiveTaxBrackets()
        {
            return await _unitOfWork.ProgressiveTaxBracketRepository.GetAllAsync();
        }
    }
}
