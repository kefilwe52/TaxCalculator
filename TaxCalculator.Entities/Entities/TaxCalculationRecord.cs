using System.ComponentModel.DataAnnotations.Schema;

namespace TaxCalculator.Entities.Entities
{
    public class TaxCalculationRecord : Entity<int>
    {
        public decimal Income { get; set; }
        public decimal TaxAmount { get; set; }
        [ForeignKey("PostalCodeTaxTypeId")]
        public int PostalCodeTaxTypeId { get; set; }
        public PostalCodeTaxType PostalCodeTaxType { get; set; }
    }
}
