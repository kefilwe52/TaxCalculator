using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Service.Calculations;

public class ProgressiveTaxCalculator : ITaxCalculator
{
    private readonly IEnumerable<ProgressiveTaxRate> _taxRates;

    public ProgressiveTaxCalculator(IEnumerable<ProgressiveTaxRate> taxRates)
    {
        _taxRates = taxRates;
    }

    public decimal CalculateTax(decimal income)
    {
        decimal tax = 0;
        var taxableIncome = income;

        foreach (var rate in _taxRates.OrderBy(r => r.TaxBracketStart))
        {
            if (taxableIncome <= 0) break;

            if (taxableIncome > rate.TaxBracketEnd - rate.TaxBracketStart)
            {
                tax += (rate.TaxBracketEnd - rate.TaxBracketStart) * rate.RatePercent;
                taxableIncome -= rate.TaxBracketEnd - rate.TaxBracketStart;
            }
            else
            {
                tax += taxableIncome * rate.RatePercent;
                taxableIncome = 0;
            }
        }

        return tax;
    }
}