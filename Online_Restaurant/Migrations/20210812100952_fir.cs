using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_Restaurant.Migrations
{
    public partial class fir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_City_CitiesCity_Id",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_CitiesCity_Id",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "CitiesCity_Id",
                table: "Restaurant");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_City_Id",
                table: "Restaurant",
                column: "City_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_City_City_Id",
                table: "Restaurant",
                column: "City_Id",
                principalTable: "City",
                principalColumn: "City_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_City_City_Id",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_City_Id",
                table: "Restaurant");

            migrationBuilder.AddColumn<int>(
                name: "CitiesCity_Id",
                table: "Restaurant",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_CitiesCity_Id",
                table: "Restaurant",
                column: "CitiesCity_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_City_CitiesCity_Id",
                table: "Restaurant",
                column: "CitiesCity_Id",
                principalTable: "City",
                principalColumn: "City_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
