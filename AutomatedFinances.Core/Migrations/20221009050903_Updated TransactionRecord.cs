using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomatedFinances.Core.Migrations
{
    public partial class UpdatedTransactionRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "FinancialTransactionRecord");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FinancialTransactionRecord",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
