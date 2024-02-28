using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CylindricalPipeHeatLoss.API.Migrations
{
    /// <inheritdoc />
    public partial class MaterialsFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diameters_Reports_ReportID_ReportGeneratedAt",
                table: "Diameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diameters",
                table: "Diameters");

            migrationBuilder.DropColumn(
                name: "Trademark",
                table: "Materials");

            migrationBuilder.RenameTable(
                name: "Diameters",
                newName: "Radiuses");

            migrationBuilder.RenameIndex(
                name: "IX_Diameters_ReportID_ReportGeneratedAt",
                table: "Radiuses",
                newName: "IX_Radiuses_ReportID_ReportGeneratedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Materials",
                type: "VARCHAR(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(128)");

            migrationBuilder.AddColumn<int>(
                name: "MaterialGroupID",
                table: "Materials",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Radiuses",
                table: "Radiuses",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "MaterialGroupDB",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialGroupDB", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialGroupID",
                table: "Materials",
                column: "MaterialGroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MaterialGroupDB_MaterialGroupID",
                table: "Materials",
                column: "MaterialGroupID",
                principalTable: "MaterialGroupDB",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Radiuses_Reports_ReportID_ReportGeneratedAt",
                table: "Radiuses",
                columns: new[] { "ReportID", "ReportGeneratedAt" },
                principalTable: "Reports",
                principalColumns: new[] { "ID", "GeneratedAt" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MaterialGroupDB_MaterialGroupID",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Radiuses_Reports_ReportID_ReportGeneratedAt",
                table: "Radiuses");

            migrationBuilder.DropTable(
                name: "MaterialGroupDB");

            migrationBuilder.DropIndex(
                name: "IX_Materials_MaterialGroupID",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Radiuses",
                table: "Radiuses");

            migrationBuilder.DropColumn(
                name: "MaterialGroupID",
                table: "Materials");

            migrationBuilder.RenameTable(
                name: "Radiuses",
                newName: "Diameters");

            migrationBuilder.RenameIndex(
                name: "IX_Radiuses_ReportID_ReportGeneratedAt",
                table: "Diameters",
                newName: "IX_Diameters_ReportID_ReportGeneratedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Materials",
                type: "VARCHAR(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(32)");

            migrationBuilder.AddColumn<string>(
                name: "Trademark",
                table: "Materials",
                type: "VARCHAR(8)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diameters",
                table: "Diameters",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Diameters_Reports_ReportID_ReportGeneratedAt",
                table: "Diameters",
                columns: new[] { "ReportID", "ReportGeneratedAt" },
                principalTable: "Reports",
                principalColumns: new[] { "ID", "GeneratedAt" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
