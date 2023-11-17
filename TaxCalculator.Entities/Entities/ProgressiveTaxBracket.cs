namespace TaxCalculator.Entities.Entities
{
    public class ProgressiveTaxBracket : Entity<int>
    {
        public decimal Rate { get; set; }
        public decimal FromIncome { get; set; }
        public decimal? ToIncome { get; set; }
    }
}
