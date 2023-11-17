using Moq;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository;
using TaxCalculator.Service.BusinessServices;
using Xunit;

namespace TaxCalculator.Test
{
    public class PostalCodeTaxTypeServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly PostalCodeTaxTypeService _service;
        public PostalCodeTaxTypeServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new PostalCodeTaxTypeService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetPostalCodeTaxType_ReturnsCorrectType()
        {
            // Arrange
            var postalCodeId = 1;
            var expected = new PostalCodeTaxType { Id = postalCodeId, PostalCode = "7441", TaxCalculationType = "Progressive" };
            _mockUnitOfWork.Setup(uow => uow.PostalCodeTaxTypeRepository.FindByAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<PostalCodeTaxType, bool>>>()))
                           .ReturnsAsync(expected);

            // Act
            var result = await _service.GetPostalCodeTaxType(postalCodeId);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task PostalCodeTaxTypes_ReturnsAllTypes()
        {
            // Arrange
            var expected = new List<PostalCodeTaxType>
            {
                new() { Id = 1,PostalCode = "7441", TaxCalculationType = "Progressive" },
                new() { Id = 1,PostalCode = "A100", TaxCalculationType = "FlatValue" },
                new(){ Id = 1,PostalCode = "7000", TaxCalculationType = "Flat rate" },
                new() { Id = 4,PostalCode = "1000", TaxCalculationType = "Progressive" },
            };
            _mockUnitOfWork.Setup(uow => uow.PostalCodeTaxTypeRepository.GetAllAsync())
                           .ReturnsAsync(expected);

            // Act
            var result = await _service.PostalCodeTaxTypes();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
