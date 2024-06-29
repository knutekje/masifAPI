using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace masifAPI.Migrations.Report
{
    /// <inheritdoc />
    public partial class updatereport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReportImgPath",
                table: "Reports",
                newName: "PictureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureId",
                table: "Reports",
                newName: "ReportImgPath");
        }
    }
}
