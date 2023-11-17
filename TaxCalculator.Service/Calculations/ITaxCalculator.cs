namespace TaxCalculator.Service.Calculations
{
    public interface ITaxCalculator
    {
        decimal CalculateTax(decimal income);
    }
}
