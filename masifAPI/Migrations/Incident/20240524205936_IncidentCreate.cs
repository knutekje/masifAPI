using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace masifAPI.Migrations.Incident
{
    /// <inheritdoc />
    public partial class IncidentCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    ReportID = table.Column<long>(type: "bigint", nullable: false),
                    IdentityUser = table.Column<long>(type: "bigint", nullable: false),
                    FoodID = table.Column<long>(type: "bigint", nullable: false),
                    ItemPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValueIncident = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, computedColumnSql: "2 * [ItemPrice]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => new { x.ReportID, x.IdentityUser });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incidents");
        }
    }
}
