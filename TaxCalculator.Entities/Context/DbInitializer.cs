using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Entities.Context
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<DataContext>();
            context?.Database.Migrate();
        }

        public void SeedData()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<DataContext>();
            if (context != null && !context.PostalCodeTaxTypes.Any())
            {
                var postalCodes = new List<PostalCodeTaxType>
            {
                new() {  PostalCode = "7441", TaxCalculationType = "Progressive" },
                new() {  PostalCode = "A100", TaxCalculationType = "Progressive"},
                new() {  PostalCode = "7000", TaxCalculationType = "FlatRate" },
                new() {  PostalCode = "1000", TaxCalculationType = "Progressive" }
            };
                context.PostalCodeTaxTypes.AddRange(postalCodes);
            }

            if (context != null && !context.ProgressiveTaxBrackets.Any())
            {
                var progressiveTaxRates = new List<ProgressiveTaxBracket>
            {
                new() { Rate = 0.1M, FromIncome = 0, ToIncome = 8350 },
                new() {  Rate = 0.15M, FromIncome = 8351, ToIncome = 33950 },
                new() {  Rate = 0.25M, FromIncome = 33951, ToIncome = 82250 },
                new() { Rate = 0.28M, FromIncome = 82251, ToIncome = 171550 },
                new() {  Rate = 0.33M, FromIncome = 171551, ToIncome = 372950 },
                new() {  Rate = 0.35M, FromIncome = 372951, ToIncome = null},
            };
                context.ProgressiveTaxBrackets.AddRange(progressiveTaxRates);
            }

            context?.SaveChanges();
        }
    }
}
