using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculator.Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialTaxCalculatorCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostalCodeInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlatValueTax = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    FlatRateTax = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodeInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgressiveTaxRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PostalCodeInfoId = table.Column<int>(type: "int", nullable: false),
                    RatePercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TaxBracketStart = table.Column<decimal>(type: "decimal(38,2)", nullable: false),
                    TaxBracketEnd = table.Column<decimal>(type: "decimal(38,2)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressiveTaxRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressiveTaxRate_PostalCodeInfo_PostalCodeInfoId",
                        column: x => x.PostalCodeInfoId,
                        principalTable: "PostalCodeInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxCalculation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCodeInfoId = table.Column<int>(type: "int", nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "decimal(38,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,10)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxCalculation_PostalCodeInfo_PostalCodeInfoId",
                        column: x => x.PostalCodeInfoId,
                        principalTable: "PostalCodeInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressiveTaxRate_PostalCodeInfoId",
                table: "ProgressiveTaxRate",
                column: "PostalCodeInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCalculation_PostalCodeInfoId",
                table: "TaxCalculation",
                column: "PostalCodeInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgressiveTaxRate");

            migrationBuilder.DropTable(
                name: "TaxCalculation");

            migrationBuilder.DropTable(
                name: "PostalCodeInfo");
        }
    }
}
