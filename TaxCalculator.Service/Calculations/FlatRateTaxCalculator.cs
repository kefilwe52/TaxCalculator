namespace TaxCalculator.Service.Calculations;

public class FlatRateTaxCalculator : ITaxCalculator
{
    private readonly decimal _taxRate;

    public FlatRateTaxCalculator(decimal taxRate)
    {
        _taxRate = taxRate;
    }

    public decimal CalculateTax(decimal income)
    {
        return income * _taxRate;
    }
}