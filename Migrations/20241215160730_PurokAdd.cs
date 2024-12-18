using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrgyLink.Migrations
{
    /// <inheritdoc />
    public partial class PurokAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Purok",
                table: "Residents");

            migrationBuilder.AddColumn<int>(
                name: "PurokId",
                table: "Residents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Puroks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puroks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Residents_PurokId",
                table: "Residents",
                column: "PurokId");

            migrationBuilder.AddForeignKey(
                name: "FK_Residents_Puroks_PurokId",
                table: "Residents",
                column: "PurokId",
                principalTable: "Puroks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Residents_Puroks_PurokId",
                table: "Residents");

            migrationBuilder.DropTable(
                name: "Puroks");

            migrationBuilder.DropIndex(
                name: "IX_Residents_PurokId",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "PurokId",
                table: "Residents");

            migrationBuilder.AddColumn<string>(
                name: "Purok",
                table: "Residents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
