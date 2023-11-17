namespace TaxCalculator.Entities.Context
{
    public interface IDbInitializer
    {
        void Initialize();
        void SeedData();
    }
}
