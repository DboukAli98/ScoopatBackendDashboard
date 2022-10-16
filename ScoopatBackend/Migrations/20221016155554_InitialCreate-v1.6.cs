using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoopatBackend.Migrations
{
    public partial class InitialCreatev16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "Employees");
        }
    }
}
