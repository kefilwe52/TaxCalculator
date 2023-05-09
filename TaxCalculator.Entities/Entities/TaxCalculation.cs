using System.ComponentModel.DataAnnotations.Schema;

namespace TaxCalculator.Entities.Entities;

public class TaxCalculation : Entity<int>
{
    [ForeignKey("PostalCodeId")] 
    public int PostalCodeInfoId { get; set; }
    public PostalCodeInfo PostalCodeInfo { get; set; }
    public decimal AnnualIncome { get; set; }
    public decimal TaxAmount { get; set; }
}