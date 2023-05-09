using System.ComponentModel.DataAnnotations.Schema;

namespace TaxCalculator.Entities.Entities;

public class ProgressiveTaxRate : Entity<int>
{
    [ForeignKey("PostalCodeId")] 
    public int PostalCodeInfoId { get; set; }

    public PostalCodeInfo PostalCodeInfo { get; set; }
    public decimal RatePercent { get; set; }
    public decimal TaxBracketStart { get; set; }
    public decimal TaxBracketEnd { get; set; }
}