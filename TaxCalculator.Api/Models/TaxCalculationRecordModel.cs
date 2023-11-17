namespace TaxCalculator.Api.Models
{
    public class TaxCalculationRecordModel
    {
        public int Id { get; set; }
        public decimal Income { get; set; }
        public decimal TaxAmount { get; set; }
        public int PostalCodeTaxTypeId { get; set; }
    }
}
