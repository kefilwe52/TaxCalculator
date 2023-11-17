namespace TaxCalculator.Entities.Entities
{
    public class PostalCodeTaxType : Entity<int>
    {
        public string PostalCode { get; set; }
        public string TaxCalculationType { get; set; }
        public ICollection<TaxCalculationRecord> TaxCalculationRecord { get; set; }
    }
}
