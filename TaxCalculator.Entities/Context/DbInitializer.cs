using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Entities.Context;

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
        if (context != null && !context.PostalCodeInfo.Any())
        {
            var postalCodes = new List<PostalCodeInfo>
            {
                new() { Id = 1, Code = "7441", CalculationType = "Progressive" },
                new() { Id = 2, Code = "A100", CalculationType = "FlatValue", FlatValueTax = 10000 },
                new() { Id = 3, Code = "7000", CalculationType = "FlatRate", FlatRateTax = 0.175m },
                new() { Id = 4, Code = "1000", CalculationType = "Progressive" }
            };
            context.PostalCodeInfo.AddRange(postalCodes);
        }

        if (context != null && !context.ProgressiveTaxRate.Any())
        {
            var progressiveTaxRates = new List<ProgressiveTaxRate>
            {
                new() { Id = 1, PostalCodeInfoId = 1, RatePercent = 0.1M, TaxBracketStart = 0, TaxBracketEnd = 8350 },
                new() { Id = 2, PostalCodeInfoId = 1, RatePercent = 0.15M, TaxBracketStart = 8351, TaxBracketEnd = 33950 },
                new() { Id = 3, PostalCodeInfoId = 1, RatePercent = 0.25M, TaxBracketStart = 33951, TaxBracketEnd = 82250 },
                new() { Id = 4, PostalCodeInfoId = 1, RatePercent = 0.28M, TaxBracketStart = 82251, TaxBracketEnd = 171550 },
                new() { Id = 5, PostalCodeInfoId = 1, RatePercent = 0.33M, TaxBracketStart = 171551, TaxBracketEnd = 372950 },
                new() { Id = 6, PostalCodeInfoId = 1, RatePercent = 0.35M, TaxBracketStart = 372951, TaxBracketEnd = 7922816251426433.5M},
                new() { Id = 7, PostalCodeInfoId = 4, RatePercent = 0.1M, TaxBracketStart = 0, TaxBracketEnd = 8350 },
                new() { Id = 8, PostalCodeInfoId = 4, RatePercent = 0.15M, TaxBracketStart = 8351, TaxBracketEnd = 33950 },
                new() { Id = 9, PostalCodeInfoId = 4, RatePercent = 0.25M, TaxBracketStart = 33951, TaxBracketEnd = 82250 },
                new() { Id = 10, PostalCodeInfoId = 4, RatePercent = 0.28M, TaxBracketStart = 82251, TaxBracketEnd = 171550 },
                new() { Id = 11, PostalCodeInfoId = 4, RatePercent = 0.33M, TaxBracketStart = 171551, TaxBracketEnd = 372950 },
                new() { Id = 12, PostalCodeInfoId = 4, RatePercent = 0.35M, TaxBracketStart = 372951, TaxBracketEnd = 7922816251426433.5M}
            };
            context.ProgressiveTaxRate.AddRange(progressiveTaxRates);
        }

       
        context?.SaveChanges();
    }
}