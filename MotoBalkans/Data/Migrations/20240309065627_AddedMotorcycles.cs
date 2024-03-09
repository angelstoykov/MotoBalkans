using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoBalkans.Data.Migrations
{
    public partial class AddedMotorcycles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Engine",
                columns: new[] { "Id", "Consumption", "EngineType", "Size" },
                values: new object[] { 1, 0, 10, 0 });

            migrationBuilder.InsertData(
                table: "Transmission",
                columns: new[] { "Id", "TransmissionType" },
                values: new object[] { 1, 10 });

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "Brand", "EngineId", "Model", "TransmissionId" },
                values: new object[] { 1, "Honda", 1, "TransAlp", 1 });

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "Brand", "EngineId", "Model", "TransmissionId" },
                values: new object[] { 2, "BMW", 1, "F800GS Adventure", 1 });

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "Brand", "EngineId", "Model", "TransmissionId" },
                values: new object[] { 3, "Yamaha", 1, "Tenere 700", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Engine",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transmission",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
