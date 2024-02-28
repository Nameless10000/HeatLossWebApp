using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CylindricalPipeHeatLoss.API.Migrations
{
    /// <inheritdoc />
    public partial class MaterialsAndMaterialGroups_Data_Filling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MaterialGroupDB",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Шамотные огнеупоры общего назначения" },
                    { 2, "Шамоты легковесные" },
                    { 3, "Корундовые обычные" },
                    { 4, "Корундовые легковесы" },
                    { 5, "Магнезитовые (периклазовые)" },
                    { 6, "Периклазошпинелидные хромитовые" },
                    { 7, "Хромитопериклазовые" },
                    { 8, "Вата" },
                    { 9, "Рулонный материал" },
                    { 10, "Войлок" },
                    { 11, "Плиты на органической связке" },
                    { 12, "Плиты на неорганической связке" },
                    { 13, "Бумага" },
                    { 14, "Картон" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "ID", "ACoeff", "BCoeff", "CCoeff", "MaterialGroupID", "Name" },
                values: new object[,]
                {
                    { 1, -8.9999999999999995E-09, 0.00037199999999999999, 0.97399999999999998, 1, "ША" },
                    { 2, 0.0, 0.00059000000000000003, 0.80400000000000005, 1, "ШБ" },
                    { 3, 0.0, 0.00035, 0.46999999999999997, 2, "ШЛ-1,3" },
                    { 4, 0.0, 0.00034699999999999998, 0.48999999999999999, 2, "ШКЛ-1,3" },
                    { 5, 0.0, 0.00035100000000000002, 0.34999999999999998, 2, "ШЛ-1,0" },
                    { 6, -8.9999999999999995E-09, 0.00023800000000000001, 0.46999999999999997, 2, "ШЛА-1,3" },
                    { 7, -2.7E-08, 0.00022699999999999999, 0.307, 2, "ШЛА-1,0" },
                    { 8, -8.9999999999999995E-09, 0.000205, 0.316, 2, "ШЛ-0,9" },
                    { 9, -4.4999999999999999E-08, 0.000176, 0.20599999999999999, 2, "ШЛ-0,6" },
                    { 10, -1.7999999999999999E-08, 0.000192, 0.11899999999999999, 2, "ШЛ-0,4" },
                    { 11, 1.452E-06, -0.004398, 6.04, 3, "К" },
                    { 12, 1.86E-07, -0.0061000000000000004, 1.409, 4, "КЛ-1,8" },
                    { 13, 9.9999999999999995E-08, -0.00025900000000000001, 0.89700000000000002, 4, "КЛ-1,3" },
                    { 14, 2.2000000000000001E-06, -0.0061000000000000004, 6.8399999999999999, 5, "МО-91" },
                    { 15, 2.12E-06, -0.0058999999999999999, 6.6100000000000003, 5, "М-3" },
                    { 16, 4.3699999999999997E-06, -0.0124, 12.1, 5, "МУ-89" },
                    { 17, 5.0200000000000002E-06, -0.010999999999999999, 10.6, 5, "М-4" },
                    { 18, 4.4800000000000003E-06, -0.011299999999999999, 12.1, 5, "М-6" },
                    { 19, 4.4000000000000002E-06, -0.0117, 12.199999999999999, 5, "М-7" },
                    { 20, 4.4000000000000002E-06, -0.0117, 12.1, 5, "М-8" },
                    { 21, 4.5000000000000001E-06, -0.0118, 12.0, 5, "М-9" },
                    { 22, 5.5899999999999998E-06, -0.0149, 14.0, 5, "МУ-91" },
                    { 23, 4.9599999999999999E-06, -0.014800000000000001, 14.4, 5, "МУ-92" },
                    { 24, 2.2000000000000001E-06, -0.0061000000000000004, 6.8399999999999999, 5, "МО-89" },
                    { 25, 4.4100000000000001E-06, -0.0118, 12.0, 5, "МГ" },
                    { 26, 2.3999999999999998E-07, -0.00131, 4.25, 6, "ПХСП" },
                    { 27, 9.7999999999999993E-07, -0.0033300000000000001, 4.7699999999999996, 6, "ПХСУТ" },
                    { 28, 9.7000000000000003E-07, -0.00331, 4.7199999999999998, 6, "ПХСУ" },
                    { 29, 3.7E-07, -0.00089999999999999998, 2.8199999999999998, 6, "ПХСОТ" },
                    { 30, 4.0999999999999999E-07, -0.00091, 2.98, 6, "ПХСС" },
                    { 31, 4.3000000000000001E-07, -0.00115, 2.6800000000000002, 7, "ХМ1-1" },
                    { 32, 4.2E-07, -0.0011100000000000001, 2.6600000000000001, 7, "ХМ2-1" },
                    { 33, 4.2E-07, -0.0011000000000000001, 2.6400000000000001, 7, "ХМ3-1" },
                    { 34, 3.7E-07, -0.00101, 2.3599999999999999, 7, "ХМ3-2" },
                    { 35, 3.8000000000000001E-07, -0.00101, 2.3199999999999998, 7, "ХМ4-1" },
                    { 36, 2.3099999999999999E-07, 0.00016799999999999999, 0.045999999999999999, 8, "МКРВ-80" },
                    { 37, 1.49E-07, 0.000136, 0.047, 9, "МКРР-130" },
                    { 38, 1.3199999999999999E-07, 0.000122, 0.049000000000000002, 9, "МКРРХ-150" },
                    { 39, 1.3199999999999999E-07, 0.000122, 0.049000000000000002, 9, "МКЦ-150" },
                    { 40, 1.06E-07, 9.6000000000000002E-05, 0.055, 10, "МКРВ-200" },
                    { 41, 7.0000000000000005E-08, 6.8999999999999997E-05, 0.079000000000000001, 11, "МКРП-340" },
                    { 42, 2E-08, 0.00015899999999999999, 0.126, 12, "МКРП-450" },
                    { 43, 4.9999999999999998E-08, 6.7999999999999999E-05, 0.108, 13, "МКРБ-500" },
                    { 44, 4.9999999999999998E-08, 6.7999999999999999E-05, 0.108, 14, "МКРК-500" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MaterialGroupDB",
                keyColumn: "ID",
                keyValue: 14);
        }
    }
}
