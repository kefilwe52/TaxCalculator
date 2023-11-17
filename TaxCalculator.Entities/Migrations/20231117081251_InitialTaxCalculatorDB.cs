using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculator.Entities.Migrations
{
    public partial class InitialTaxCalculatorDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostalCodeTaxTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCalculationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodeTaxTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgressiveTaxBrackets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    FromIncome = table.Column<decimal>(type: "decimal(38,2)", nullable: false),
                    ToIncome = table.Column<decimal>(type: "decimal(38,2)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressiveTaxBrackets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxCalculationRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Income = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PostalCodeTaxTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculationRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxCalculationRecords_PostalCodeTaxTypes_PostalCodeTaxTypeId",
                        column: x => x.PostalCodeTaxTypeId,
                        principalTable: "PostalCodeTaxTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxCalculationRecords_PostalCodeTaxTypeId",
                table: "TaxCalculationRecords",
                column: "PostalCodeTaxTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgressiveTaxBrackets");

            migrationBuilder.DropTable(
                name: "TaxCalculationRecords");

            migrationBuilder.DropTable(
                name: "PostalCodeTaxTypes");
        }
    }
}
