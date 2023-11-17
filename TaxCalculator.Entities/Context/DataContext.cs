using Microsoft.EntityFrameworkCore;
using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Entities.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        public DbSet<PostalCodeTaxType> PostalCodeTaxTypes { get; set; }
        public DbSet<ProgressiveTaxBracket> ProgressiveTaxBrackets { get; set; }
        public DbSet<TaxCalculationRecord> TaxCalculationRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxCalculationRecord>()
                .Property(p => p.Income)
                .HasColumnType("decimal(18,6)");

            modelBuilder.Entity<TaxCalculationRecord>()
                .Property(p => p.TaxAmount)
                .HasColumnType("decimal(18,6)");

            modelBuilder.Entity<ProgressiveTaxBracket>()
                .Property(p => p.Rate)
                .HasColumnType("decimal(5, 2)");

            modelBuilder.Entity<ProgressiveTaxBracket>()
                .Property(p => p.ToIncome)
                .HasColumnType("decimal(38, 2)");

            modelBuilder.Entity<ProgressiveTaxBracket>()
                .Property(p => p.FromIncome)
                .HasColumnType("decimal(38, 2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
