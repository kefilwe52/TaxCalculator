namespace TaxCalculator.Entities.Entities;

public class PostalCodeInfo : Entity<int>
{
    public string Code { get; set; }
    public string CalculationType { get; set; }
    public decimal? FlatValueTax { get; set; }
    public decimal? FlatRateTax { get; set; }
}