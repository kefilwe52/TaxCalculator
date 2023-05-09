namespace TaxCalculator.Service.Calculations;

public class FlatValueTaxCalculator : ITaxCalculator
{
    private readonly decimal _taxValue;

    public FlatValueTaxCalculator(decimal taxValue)
    {
        _taxValue = taxValue;
    }

    public decimal CalculateTax(decimal income)
    {
        return _taxValue;
    }
}