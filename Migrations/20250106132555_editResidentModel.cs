using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrgyLink.Migrations
{
    /// <inheritdoc />
    public partial class editResidentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurokId",
                table: "Puroks",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurokId",
                table: "Puroks");
        }
    }
}
