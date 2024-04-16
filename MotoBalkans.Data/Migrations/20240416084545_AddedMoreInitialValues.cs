using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoBalkans.Data.Migrations
{
    public partial class AddedMoreInitialValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Engines",
                columns: new[] { "Id", "Consumption", "EngineType", "Size" },
                values: new object[] { 2, 0, 20, 0 });

            migrationBuilder.InsertData(
                table: "Engines",
                columns: new[] { "Id", "Consumption", "EngineType", "Size" },
                values: new object[] { 3, 0, 30, 0 });

            migrationBuilder.InsertData(
                table: "Transmissions",
                columns: new[] { "Id", "TransmissionType" },
                values: new object[] { 2, 20 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transmissions",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
