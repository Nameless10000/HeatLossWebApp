using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CylindricalPipeHeatLoss.API.Migrations
{
    /// <inheritdoc />
    public partial class materialgroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MaterialGroupDB_MaterialGroupID",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialGroupDB",
                table: "MaterialGroupDB");

            migrationBuilder.RenameTable(
                name: "MaterialGroupDB",
                newName: "MaterialGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialGroups",
                table: "MaterialGroups",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MaterialGroups_MaterialGroupID",
                table: "Materials",
                column: "MaterialGroupID",
                principalTable: "MaterialGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MaterialGroups_MaterialGroupID",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialGroups",
                table: "MaterialGroups");

            migrationBuilder.RenameTable(
                name: "MaterialGroups",
                newName: "MaterialGroupDB");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialGroupDB",
                table: "MaterialGroupDB",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MaterialGroupDB_MaterialGroupID",
                table: "Materials",
                column: "MaterialGroupID",
                principalTable: "MaterialGroupDB",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
