using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomatedFinances.Core.Migrations
{
    public partial class Dropedgenericentityandprototypecode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenericTransaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenericTransaction",
                columns: table => new
                {
                    TrackingId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    From = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OccuredAtDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecordedAtDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SavedAtDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SavedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    To = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericTransaction", x => x.TrackingId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
