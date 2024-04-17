using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoBalkans.Data.Migrations
{
    public partial class addedsomepictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Motorcycles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: 1,
                column: "PictureUrl",
                value: "https://dizzyriders.bg/uploads/avtomobili/11_2022/421227_23YM_XL750_Transalp.jpg");

            migrationBuilder.UpdateData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: 2,
                column: "PictureUrl",
                value: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRjudx8BnnMOEP6Gb2zki94D0gBoFxuwbsWc_VKS0J74A&s");

            migrationBuilder.UpdateData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: 3,
                column: "PictureUrl",
                value: "https://advanywhere.com/wp-content/uploads/2022/12/AdvAnywhere-Tenere-review-1920x1280.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Motorcycles");
        }
    }
}
