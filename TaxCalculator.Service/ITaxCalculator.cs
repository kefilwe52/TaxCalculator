namespace TaxCalculator.Service;

internal interface ITaxCalculator
{
    decimal CalculateTax(decimal income);
}