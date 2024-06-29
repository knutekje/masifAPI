using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace masifAPI.Migrations.Picture
{
    /// <inheritdoc />
    public partial class updatePiture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "File",
                table: "Pictures",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Pictures");
        }
    }
}
