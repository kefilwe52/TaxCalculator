using Moq;
using TaxCalculator.Entities.Entities;
using TaxCalculator.Repository;
using TaxCalculator.Service.BusinessServices;
using Xunit;

namespace TaxCalculator.Test
{
    public class ProgressiveTaxBracketServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ProgressiveTaxBracketService _service;

        public ProgressiveTaxBracketServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new ProgressiveTaxBracketService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetProgressiveTaxBrackets_ReturnsAllBrackets()
        {
            // Arrange
            var expectedBrackets = new List<ProgressiveTaxBracket>
            {
                new() { Rate = 0.1M, FromIncome = 0, ToIncome = 8350 },
                new() {  Rate = 0.15M, FromIncome = 8351, ToIncome = 33950 },
                new() {  Rate = 0.25M, FromIncome = 33951, ToIncome = 82250 },
                new() { Rate = 0.28M, FromIncome = 82251, ToIncome = 171550 },
                new() {  Rate = 0.33M, FromIncome = 171551, ToIncome = 372950 },
                new() {  Rate = 0.35M, FromIncome = 372951, ToIncome = null},
            };

            _mockUnitOfWork.Setup(uow => uow.ProgressiveTaxBracketRepository.GetAllAsync())
                .ReturnsAsync(expectedBrackets);

            // Act
            var brackets = await _service.GetProgressiveTaxBrackets();

            // Assert
            Assert.Equal(expectedBrackets.Count, brackets.Count());
        }
    }
}
