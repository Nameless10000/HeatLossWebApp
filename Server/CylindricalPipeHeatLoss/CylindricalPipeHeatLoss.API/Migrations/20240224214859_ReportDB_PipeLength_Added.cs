using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CylindricalPipeHeatLoss.API.Migrations
{
    /// <inheritdoc />
    public partial class ReportDB_PipeLength_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PipeLength",
                table: "Reports",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PipeLength",
                table: "Reports");
        }
    }
}
