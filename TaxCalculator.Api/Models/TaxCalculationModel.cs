namespace TaxCalculator.Api.Models;

public class TaxCalculationModel
{
    public int Id { get; set; }
    public int PostalCodeInfoId { get; set; }
    public decimal AnnualIncome { get; set; }
    public decimal TaxAmount { get; set; }
}