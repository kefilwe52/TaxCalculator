﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaxCalculator.Entities.Context;

#nullable disable

namespace TaxCalculator.Entities.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TaxCalculator.Entities.Entities.PostalCodeTaxType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxCalculationType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PostalCodeTaxTypes");
                });

            modelBuilder.Entity("TaxCalculator.Entities.Entities.ProgressiveTaxBracket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FromIncome")
                        .HasColumnType("decimal(38,2)");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(5,2)");

                    b.Property<decimal?>("ToIncome")
                        .HasColumnType("decimal(38,2)");

                    b.HasKey("Id");

                    b.ToTable("ProgressiveTaxBrackets");
                });

            modelBuilder.Entity("TaxCalculator.Entities.Entities.TaxCalculationRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Income")
                        .HasColumnType("decimal(18,6)");

                    b.Property<int>("PostalCodeTaxTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("TaxAmount")
                        .HasColumnType("decimal(18,6)");

                    b.HasKey("Id");

                    b.HasIndex("PostalCodeTaxTypeId");

                    b.ToTable("TaxCalculationRecords");
                });

            modelBuilder.Entity("TaxCalculator.Entities.Entities.TaxCalculationRecord", b =>
                {
                    b.HasOne("TaxCalculator.Entities.Entities.PostalCodeTaxType", "PostalCodeTaxType")
                        .WithMany("TaxCalculationRecord")
                        .HasForeignKey("PostalCodeTaxTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostalCodeTaxType");
                });

            modelBuilder.Entity("TaxCalculator.Entities.Entities.PostalCodeTaxType", b =>
                {
                    b.Navigation("TaxCalculationRecord");
                });
#pragma warning restore 612, 618
        }
    }
}
