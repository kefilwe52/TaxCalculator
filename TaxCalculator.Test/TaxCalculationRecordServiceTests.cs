using Moq;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository;
using TaxCalculator.Service.BusinessContracts;
using TaxCalculator.Service.BusinessServices;
using Xunit;

namespace TaxCalculator.Test
{
    public class TaxCalculationRecordServiceTests
    {
        private readonly TaxCalculationRecordService _service;
        private readonly Mock<IProgressiveTaxBracketService> _mockProgressiveTaxBracketService = new();
        private readonly Mock<IPostalCodeTaxTypeService> _mockPostalCodeTaxTypeService = new();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();

        public TaxCalculationRecordServiceTests()
        {
            _service = new TaxCalculationRecordService(
                _mockProgressiveTaxBracketService.Object,
                _mockPostalCodeTaxTypeService.Object,
                _mockUnitOfWork.Object);
        }

        [Theory]
        [InlineData(50000, 7441)]
        public async Task AddTaxCalculationRecord_ProgressiveTaxType_CalculatesCorrectTax(decimal income, int postalCodeId)
        {
            // Arrange
            var postalCodeTaxType = new PostalCodeTaxType { Id = postalCodeId, TaxCalculationType = "Progressive" };
            var taxCalculationRecord = new TaxCalculationRecord { Income = income, PostalCodeTaxTypeId = postalCodeId };
            var taxBrackets = new List<ProgressiveTaxBracket>
            {
              new() { Rate = 0.1M, FromIncome = 0, ToIncome = 8350 },
                new() {  Rate = 0.15M, FromIncome = 8351, ToIncome = 33950 },
                new() {  Rate = 0.25M, FromIncome = 33951, ToIncome = 82250 },
                new() { Rate = 0.28M, FromIncome = 82251, ToIncome = 171550 },
                new() {  Rate = 0.33M, FromIncome = 171551, ToIncome = 372950 },
                new() {  Rate = 0.35M, FromIncome = 372951, ToIncome = null},
            };

            _mockPostalCodeTaxTypeService.Setup(s => s.GetPostalCodeTaxType(postalCodeId))
                .ReturnsAsync(postalCodeTaxType);
            _mockProgressiveTaxBracketService.Setup(s => s.GetProgressiveTaxBrackets())
                .ReturnsAsync(taxBrackets);
            _mockUnitOfWork.Setup(u => u.TaxCalculationRecordRepository.AddAsync(It.IsAny<TaxCalculationRecord>()))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.Commit()).Verifiable();

            decimal expectedTaxAmount = 8687.10m;

            // Act
            var result = await _service.AddTaxCalculationRecord(taxCalculationRecord);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTaxAmount, result.TaxAmount);
            _mockUnitOfWork.Verify(u => u.Commit(), Times.Once);
        }

        [Theory]
        [InlineData(300000)]
        [InlineData(80000)]
        public async Task AddTaxCalculationRecord_FlatRateTaxType_CalculatesCorrectTax(decimal income)
        {
            // Arrange
            int postalCodeId = 7100;
            var postalCodeTaxType = new PostalCodeTaxType { Id = postalCodeId, TaxCalculationType = "FlatRate" };
            var taxCalculationRecord = new TaxCalculationRecord { Income = income, PostalCodeTaxTypeId = postalCodeId };

            _mockPostalCodeTaxTypeService.Setup(s => s.GetPostalCodeTaxType(postalCodeId))
                .ReturnsAsync(postalCodeTaxType);
            _mockUnitOfWork.Setup(u => u.TaxCalculationRecordRepository.AddAsync(It.IsAny<TaxCalculationRecord>()))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.Commit()).Verifiable();

            decimal expectedTaxAmount = income * 0.175m;

            // Act
            var result = await _service.AddTaxCalculationRecord(taxCalculationRecord);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTaxAmount, result.TaxAmount);
            _mockUnitOfWork.Verify(u => u.Commit(), Times.Once);
        }

        [Theory]
        [InlineData(550000)]
        [InlineData(100000)]
        public async Task AddTaxCalculationRecord_FlatValueTaxType_CalculatesCorrectTax(decimal income)
        {
            // Arrange
            int postalCodeId = 2;
            var postalCodeTaxType = new PostalCodeTaxType { Id = postalCodeId, TaxCalculationType = "FlatValue" };
            var taxCalculationRecord = new TaxCalculationRecord { Income = income, PostalCodeTaxTypeId = postalCodeId };

            _mockPostalCodeTaxTypeService.Setup(s => s.GetPostalCodeTaxType(postalCodeId))
                .ReturnsAsync(postalCodeTaxType);
            _mockUnitOfWork.Setup(u => u.TaxCalculationRecordRepository.AddAsync(It.IsAny<TaxCalculationRecord>()))
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.Commit()).Verifiable();

            decimal expectedTaxAmount = income < 200000 ? income * 0.05m : 10000m;

            // Act
            var result = await _service.AddTaxCalculationRecord(taxCalculationRecord);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTaxAmount, result.TaxAmount);
            _mockUnitOfWork.Verify(u => u.Commit(), Times.Once);
        }

    }
}
