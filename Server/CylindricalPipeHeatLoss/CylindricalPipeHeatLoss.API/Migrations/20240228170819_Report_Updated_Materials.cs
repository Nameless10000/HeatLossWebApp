using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CylindricalPipeHeatLoss.API.Migrations
{
    /// <inheritdoc />
    public partial class Report_Updated_Materials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThermalResistances");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "PipeLayers");

            migrationBuilder.DropColumn(
                name: "ThermalConductivityCoeff",
                table: "PipeLayers");

            migrationBuilder.RenameColumn(
                name: "TotalHeatLoss_Q",
                table: "Reports",
                newName: "ql");

            migrationBuilder.RenameColumn(
                name: "PipeThermalResistance_Rl",
                table: "Reports",
                newName: "e");

            migrationBuilder.RenameColumn(
                name: "LinearHeatTransferCoefficient_kl",
                table: "Reports",
                newName: "a2");

            migrationBuilder.RenameColumn(
                name: "LinearHeatFlowDensity_ql",
                table: "Reports",
                newName: "a1");

            migrationBuilder.AddColumn<double>(
                name: "InnerTemp",
                table: "Reports",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OutterTemp",
                table: "Reports",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Q",
                table: "Reports",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MaterialID",
                table: "PipeLayers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ACoeff = table.Column<double>(type: "REAL", nullable: false),
                    BCoeff = table.Column<double>(type: "REAL", nullable: false),
                    CCoeff = table.Column<double>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    Trademark = table.Column<string>(type: "VARCHAR(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PipeLayers_MaterialID",
                table: "PipeLayers",
                column: "MaterialID");

            migrationBuilder.AddForeignKey(
                name: "FK_PipeLayers_Materials_MaterialID",
                table: "PipeLayers",
                column: "MaterialID",
                principalTable: "Materials",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PipeLayers_Materials_MaterialID",
                table: "PipeLayers");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_PipeLayers_MaterialID",
                table: "PipeLayers");

            migrationBuilder.DropColumn(
                name: "InnerTemp",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "OutterTemp",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Q",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "MaterialID",
                table: "PipeLayers");

            migrationBuilder.RenameColumn(
                name: "ql",
                table: "Reports",
                newName: "TotalHeatLoss_Q");

            migrationBuilder.RenameColumn(
                name: "e",
                table: "Reports",
                newName: "PipeThermalResistance_Rl");

            migrationBuilder.RenameColumn(
                name: "a2",
                table: "Reports",
                newName: "LinearHeatTransferCoefficient_kl");

            migrationBuilder.RenameColumn(
                name: "a1",
                table: "Reports",
                newName: "LinearHeatFlowDensity_ql");

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "PipeLayers",
                type: "VARCHAR(128)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "ThermalConductivityCoeff",
                table: "PipeLayers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "ThermalResistances",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportID = table.Column<int>(type: "INTEGER", nullable: false),
                    ReportGeneratedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThermalResistances", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ThermalResistances_Reports_ReportID_ReportGeneratedAt",
                        columns: x => new { x.ReportID, x.ReportGeneratedAt },
                        principalTable: "Reports",
                        principalColumns: new[] { "ID", "GeneratedAt" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThermalResistances_ReportID_ReportGeneratedAt",
                table: "ThermalResistances",
                columns: new[] { "ReportID", "ReportGeneratedAt" });
        }
    }
}
