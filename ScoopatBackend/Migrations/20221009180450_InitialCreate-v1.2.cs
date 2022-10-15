using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoopatBackend.Migrations
{
    public partial class InitialCreatev12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FarmersOwners");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FarmersOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    FarmOwnerOwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmersOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FarmersOwners_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "FarmerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarmersOwners_FarmOwners_FarmOwnerOwnerId",
                        column: x => x.FarmOwnerOwnerId,
                        principalTable: "FarmOwners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FarmersOwners_FarmerId",
                table: "FarmersOwners",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmersOwners_FarmOwnerOwnerId",
                table: "FarmersOwners",
                column: "FarmOwnerOwnerId");
        }
    }
}
