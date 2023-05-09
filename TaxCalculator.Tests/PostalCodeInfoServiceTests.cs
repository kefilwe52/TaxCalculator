using System.Linq.Expressions;
using Moq;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository;
using TaxCalculator.Service.BusinessServices;
using Xunit;

namespace TaxCalculator.Test;

public class PostalCodeInfoServiceTests
{
    private readonly PostalCodeInfoService _service;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public PostalCodeInfoServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _service = new PostalCodeInfoService(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task GetPostalCodeInfoAsync_ReturnsPostalCodeInfo_WhenPostalCodeExists()
    {
        // Arrange
        var postalCode = "12345";
        var postalCodeInfo = new PostalCodeInfo { Code = postalCode };
        _unitOfWorkMock
            .Setup(x => x.PostalCodeInfoRepository.FindOneAsync(It.IsAny<Expression<Func<PostalCodeInfo, bool>>>()))
            .ReturnsAsync(postalCodeInfo);

        // Act
        var result = await _service.GetPostalCodeInfoAsync(postalCode);

        // Assert
        Assert.Equal(postalCodeInfo, result);
    }

    [Fact]
    public async Task GetPostalCodeInfoAsync_ReturnsNull_WhenPostalCodeDoesNotExist()
    {
        // Arrange
        var postalCode = "54321";
        _unitOfWorkMock
            .Setup(x => x.PostalCodeInfoRepository.FindOneAsync(It.IsAny<Expression<Func<PostalCodeInfo, bool>>>()))
            .ReturnsAsync(null as PostalCodeInfo);

        // Act
        var result = await _service.GetPostalCodeInfoAsync(postalCode);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllPostalCodeInfoAsync_ReturnsAllPostalCodeInfo()
    {
        // Arrange
        var postalCodeInfoList = new List<PostalCodeInfo>
        {
            new() { Code = "7441" },
            new() { Code = "A100" },
            new() { Code = "7000" },
            new() { Code = "1000" }
        };
        _unitOfWorkMock
            .Setup(x => x.PostalCodeInfoRepository.GetAllAsync())
            .ReturnsAsync(postalCodeInfoList);

        // Act
        var result = await _service.GetAllPostalCodeInfoAsync();

        // Assert
        Assert.Equal(postalCodeInfoList, result);
    }
}