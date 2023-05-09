using System.Linq.Expressions;
using Moq;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository;
using TaxCalculator.Service.BusinessContracts;
using TaxCalculator.Service.BusinessServices;
using Xunit;

namespace TaxCalculator.Test;

public class ProgressiveTaxRateServiceTests
{
    private readonly IProgressiveTaxRateService _progressiveTaxRateService;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public ProgressiveTaxRateServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _progressiveTaxRateService = new ProgressiveTaxRateService(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task GetProgressiveTaxRate_ShouldReturnProgressiveTaxRate_WhenPostalCodeInfoIdIsValid()
    {
        // Arrange
        var postalCodeInfoId = 1;
        var expectedProgressiveTaxRate = new ProgressiveTaxRate
        {
            PostalCodeInfoId = postalCodeInfoId,
            TaxBracketStart = 10000,
            TaxBracketEnd = 20000,
            RatePercent = 0.1m
        };

        _unitOfWorkMock.Setup(uow => uow.ProgressiveTaxRateRepository
                .FindOneAsync(It.IsAny<Expression<Func<ProgressiveTaxRate, bool>>>()))
            .ReturnsAsync(expectedProgressiveTaxRate);

        // Act
        var actualProgressiveTaxRate = await _progressiveTaxRateService.GetProgressiveTaxRate(postalCodeInfoId);

        // Assert
        Assert.Equal(expectedProgressiveTaxRate, actualProgressiveTaxRate);
    }
}