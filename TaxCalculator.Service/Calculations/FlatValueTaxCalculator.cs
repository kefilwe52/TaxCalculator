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
        decimal tax;

        if (income < 200000)
            tax = 0.05m * income;
        else
            tax = _taxValue;

        return tax;
    }
}