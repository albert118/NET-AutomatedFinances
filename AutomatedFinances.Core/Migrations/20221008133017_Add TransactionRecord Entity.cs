using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomatedFinances.Core.Migrations
{
    public partial class AddTransactionRecordEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialTransactionRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalCost = table.Column<long>(type: "bigint", nullable: false),
                    Reference = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OccuredAtDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecordedAtDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SavedAtDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SavedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransactionRecord", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialTransactionRecord");
        }
    }
}
