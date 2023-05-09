using System.Linq.Expressions;
using Moq;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository;
using TaxCalculator.Service.BusinessContracts;
using TaxCalculator.Service.BusinessServices;
using Xunit;

namespace TaxCalculator.Test;

public class TaxCalculationServiceTests
{
    private readonly ITaxCalculationService _taxCalculationService;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public TaxCalculationServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _taxCalculationService = new TaxCalculationService(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreateTaxCalculationAsync_ShouldReturnTaxCalculation_WhenValidInputIsProvided()
    {
        // Arrange
        var taxCalculation = new TaxCalculation
        {
            AnnualIncome = 50000,
            PostalCodeInfoId = 1
        };

        var postalCodeInfo = new PostalCodeInfo
        {
            CalculationType = "Progressive",
            Code = "7441",
            Id = 1,
            CreatedDateTime = DateTime.Now
        };


        var taxRates = new List<ProgressiveTaxRate>
        {
            new() { TaxBracketStart = 0, TaxBracketEnd = 8350, RatePercent = 0.1m },
            new() { TaxBracketStart = 8351, TaxBracketEnd = 33950, RatePercent = 0.15m },
            new() { TaxBracketStart = 33951, TaxBracketEnd = 82250, RatePercent = 0.25m },
            new() { TaxBracketStart = 82251, TaxBracketEnd = 171550, RatePercent = 0.28m },
            new() { TaxBracketStart = 171551, TaxBracketEnd = 372950, RatePercent = 0.33m },
            new() { TaxBracketStart = 372951, TaxBracketEnd = decimal.MaxValue, RatePercent = 0.35m }
        };

        _unitOfWorkMock
            .Setup(x => x.ProgressiveTaxRateRepository
                .FindByAsync(It.IsAny<Expression<Func<ProgressiveTaxRate, bool>>>()))
            .ReturnsAsync(taxRates);

        _unitOfWorkMock
            .Setup(x => x.TaxCalculationRepository
                .FindByAsync(It.IsAny<Expression<Func<TaxCalculation, bool>>>()))
            .ReturnsAsync(new List<TaxCalculation>());

        _unitOfWorkMock
            .Setup(x => x.TaxCalculationRepository
                .AddAsync(It.IsAny<TaxCalculation>()))
            .ReturnsAsync(taxCalculation);

        _unitOfWorkMock
            .Setup(x => x.PostalCodeInfoRepository
                .FindOneAsync(x => x.Id == taxCalculation.PostalCodeInfoId))
            .ReturnsAsync(postalCodeInfo);

        _unitOfWorkMock
            .Setup(x => x.Commit())
            .Returns(1);

        // Act
        var result = await _taxCalculationService.CreateTaxCalculationAsync(taxCalculation);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taxCalculation.AnnualIncome, result.AnnualIncome);
        Assert.Equal(taxCalculation.PostalCodeInfoId, result.PostalCodeInfoId);
    }

    [Fact]
    public async Task CreateTaxCalculationAsync_ShouldThrowArgumentNullException_WhenPassedNullTaxCalculation()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _taxCalculationService.CreateTaxCalculationAsync(null));
    }
}