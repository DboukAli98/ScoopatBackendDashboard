using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoopatBackend.Migrations
{
    public partial class InitialCreatev11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Farms_Farmers_FarmerId",
                table: "Farms");

            migrationBuilder.DropIndex(
                name: "IX_Farms_FarmerId",
                table: "Farms");

            migrationBuilder.DropColumn(
                name: "FarmerId",
                table: "Farms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FarmerId",
                table: "Farms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Farms_FarmerId",
                table: "Farms",
                column: "FarmerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Farms_Farmers_FarmerId",
                table: "Farms",
                column: "FarmerId",
                principalTable: "Farmers",
                principalColumn: "FarmerId");
        }
    }
}
