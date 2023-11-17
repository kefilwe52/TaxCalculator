using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository;
using TaxCalculator.Service.BusinessContracts;
using TaxCalculator.Service.Calculations;

namespace TaxCalculator.Service.BusinessServices
{
    public class TaxCalculationRecordService : ITaxCalculationRecordService
    {
        private readonly IProgressiveTaxBracketService _progressiveTaxBracketService;
        private readonly IPostalCodeTaxTypeService _postalCodeTaxTypeService;
        private readonly IUnitOfWork _unitOfWork;

        public TaxCalculationRecordService(
              IProgressiveTaxBracketService progressiveTaxBracketService,
              IPostalCodeTaxTypeService postalCodeTaxTypeService,
              IUnitOfWork unitOfWork)
        {
            _progressiveTaxBracketService = progressiveTaxBracketService;
            _postalCodeTaxTypeService = postalCodeTaxTypeService;
            _unitOfWork = unitOfWork;
        }

        public async Task<TaxCalculationRecord> AddTaxCalculationRecord(TaxCalculationRecord calculationRecord)
        {
            if (calculationRecord == null)
                throw new ArgumentException("Tax record cannot be null.");

            var postalCodeTaxType = await _postalCodeTaxTypeService.GetPostalCodeTaxType(calculationRecord.PostalCodeTaxTypeId);

            if (postalCodeTaxType == null)
                throw new ArgumentException($"Postal code not found.");

            ITaxCalculator taxCalculator = GetTaxCalculator(postalCodeTaxType.TaxCalculationType, calculationRecord.Income);

            var taxAmount = taxCalculator.CalculateTax(calculationRecord.Income);

            var taxCalculationRecord = new TaxCalculationRecord
            {
                PostalCodeTaxTypeId = postalCodeTaxType.Id,
                Income = calculationRecord.Income,
                TaxAmount = taxAmount,
                CreatedDateTime = DateTime.UtcNow,
            };

            await _unitOfWork.TaxCalculationRecordRepository.AddAsync(taxCalculationRecord);
            _unitOfWork.Commit();

            return taxCalculationRecord;
        }

        public async Task<TaxCalculationRecord> GetTaxCalculationRecord(int taxCalculationRecordId)
        {
            return await _unitOfWork.TaxCalculationRecordRepository.FindByAsync(x => x.Id == taxCalculationRecordId);
        }

        public async Task<IEnumerable<TaxCalculationRecord>> GetTaxCalculationRecords()
        {
            return await _unitOfWork.TaxCalculationRecordRepository.GetAllAsync();
        }

        private ITaxCalculator GetTaxCalculator(string taxCalculationType, decimal income)
        {
            switch (taxCalculationType)
            {
                case "FlatRate":
                    return new FlatRateTaxCalculator(0.175m);
                case "FlatValue":
                    return new FlatValueTaxCalculator(10000);
                case "Progressive":
                    var taxBrackets = _progressiveTaxBracketService.GetProgressiveTaxBrackets().Result; // Consider async handling
                    return new ProgressiveTaxCalculator(taxBrackets);
                default:
                    throw new ArgumentException("Invalid tax calculation type");
            }
        }
    }
}
