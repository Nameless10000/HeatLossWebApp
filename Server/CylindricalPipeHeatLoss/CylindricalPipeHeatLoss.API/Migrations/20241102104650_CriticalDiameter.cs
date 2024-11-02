using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CylindricalPipeHeatLoss.API.Migrations
{
    /// <inheritdoc />
    public partial class CriticalDiameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CriticalDiameter",
                table: "Reports",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriticalDiameter",
                table: "Reports");
        }
    }
}
