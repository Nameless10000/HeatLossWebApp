using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CylindricalPipeHeatLoss.API.Migrations
{
    /// <inheritdoc />
    public partial class ReportDB_ExtraFieldsRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InnerQl",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "OutterQl",
                table: "Reports");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "InnerQl",
                table: "Reports",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OutterQl",
                table: "Reports",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
