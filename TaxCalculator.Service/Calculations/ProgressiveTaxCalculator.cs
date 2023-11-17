using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Service.Calculations;

public class ProgressiveTaxCalculator : ITaxCalculator
{
    private readonly IEnumerable<ProgressiveTaxBracket> _taxRates;

    public ProgressiveTaxCalculator(IEnumerable<ProgressiveTaxBracket> taxRates)
    {
        _taxRates = taxRates.OrderBy(bracket => bracket.FromIncome);
    }

    public decimal CalculateTax(decimal income)
    {
        decimal totalTax = 0m;

        foreach (var bracket in _taxRates)
        {
            if (income <= bracket.FromIncome)
                continue;

            decimal upperLimit = bracket.ToIncome ?? income;
            decimal taxableAmountInBracket = Math.Min(upperLimit, income) - bracket.FromIncome;

            decimal taxForBracket = taxableAmountInBracket * bracket.Rate;

            totalTax += taxForBracket;

            if (upperLimit >= income)
                break;
        }

        return totalTax;
    }
}