using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Service.BusinessContracts
{
    public interface IProgressiveTaxBracketService
    {
        Task<IEnumerable<ProgressiveTaxBracket>> GetProgressiveTaxBrackets();
    }
}
