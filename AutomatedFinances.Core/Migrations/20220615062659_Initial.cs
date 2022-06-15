using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomatedFinances.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenericTransaction",
                columns: table => new
                {
                    TrackingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccuredAtDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecordedAtDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SavedAtDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SavedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericTransaction", x => x.TrackingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenericTransaction");
        }
    }
}
