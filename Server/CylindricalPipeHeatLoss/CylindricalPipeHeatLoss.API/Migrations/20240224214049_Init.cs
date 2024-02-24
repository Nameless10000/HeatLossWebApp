using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CylindricalPipeHeatLoss.API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalHeatLoss_Q = table.Column<double>(type: "REAL", nullable: false),
                    LinearHeatFlowDensity_ql = table.Column<double>(type: "REAL", nullable: false),
                    LinearHeatTransferCoefficient_kl = table.Column<double>(type: "REAL", nullable: false),
                    PipeThermalResistance_Rl = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => new { x.ID, x.GeneratedAt });
                });

            migrationBuilder.CreateTable(
                name: "Diameters",
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
                    table.PrimaryKey("PK_Diameters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Diameters_Reports_ReportID_ReportGeneratedAt",
                        columns: x => new { x.ReportID, x.ReportGeneratedAt },
                        principalTable: "Reports",
                        principalColumns: new[] { "ID", "GeneratedAt" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PipeLayers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportID = table.Column<int>(type: "INTEGER", nullable: false),
                    ReportGeneratedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Material = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    Width = table.Column<double>(type: "REAL", nullable: false),
                    ThermalConductivityCoeff = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipeLayers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PipeLayers_Reports_ReportID_ReportGeneratedAt",
                        columns: x => new { x.ReportID, x.ReportGeneratedAt },
                        principalTable: "Reports",
                        principalColumns: new[] { "ID", "GeneratedAt" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Temperatures",
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
                    table.PrimaryKey("PK_Temperatures", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Temperatures_Reports_ReportID_ReportGeneratedAt",
                        columns: x => new { x.ReportID, x.ReportGeneratedAt },
                        principalTable: "Reports",
                        principalColumns: new[] { "ID", "GeneratedAt" },
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Diameters_ReportID_ReportGeneratedAt",
                table: "Diameters",
                columns: new[] { "ReportID", "ReportGeneratedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_PipeLayers_ReportID_ReportGeneratedAt",
                table: "PipeLayers",
                columns: new[] { "ReportID", "ReportGeneratedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Temperatures_ReportID_ReportGeneratedAt",
                table: "Temperatures",
                columns: new[] { "ReportID", "ReportGeneratedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ThermalResistances_ReportID_ReportGeneratedAt",
                table: "ThermalResistances",
                columns: new[] { "ReportID", "ReportGeneratedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diameters");

            migrationBuilder.DropTable(
                name: "PipeLayers");

            migrationBuilder.DropTable(
                name: "Temperatures");

            migrationBuilder.DropTable(
                name: "ThermalResistances");

            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
