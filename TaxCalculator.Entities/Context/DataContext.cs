using Microsoft.EntityFrameworkCore;
using TaxCalculator.Entities.Entities;

namespace TaxCalculator.Entities.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<PostalCodeInfo> PostalCodeInfo { get; set; }
    public DbSet<ProgressiveTaxRate> ProgressiveTaxRate { get; set; }
    public DbSet<TaxCalculation> TaxCalculation { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PostalCodeInfo>()
            .Property(p => p.FlatRateTax)
            .HasColumnType("decimal(18,6)");

        modelBuilder.Entity<PostalCodeInfo>()
            .Property(p => p.FlatValueTax)
            .HasColumnType("decimal(18,6)");

        modelBuilder.Entity<ProgressiveTaxRate>()
            .Property(p => p.RatePercent)
            .HasColumnType("decimal(5, 2)");

        modelBuilder.Entity<ProgressiveTaxRate>()
            .Property(p => p.TaxBracketStart)
            .HasColumnType("decimal(38, 2)");

        modelBuilder.Entity<ProgressiveTaxRate>()
            .Property(p => p.TaxBracketEnd)
            .HasColumnType("decimal(38, 2)");

        modelBuilder.Entity<TaxCalculation>()
            .Property(p => p.AnnualIncome)
            .HasColumnType("decimal(38, 2)");

        modelBuilder.Entity<TaxCalculation>()
            .Property(p => p.TaxAmount)
            .HasColumnType("decimal(18, 10)");

        modelBuilder.Entity<PostalCodeInfo>()
            .Property(p => p.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<ProgressiveTaxRate>()
            .Property(p => p.Id)
            .ValueGeneratedNever();
    }
}