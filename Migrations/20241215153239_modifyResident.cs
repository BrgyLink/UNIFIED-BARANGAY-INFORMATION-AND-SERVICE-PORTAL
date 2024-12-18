using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrgyLink.Migrations
{
    /// <inheritdoc />
    public partial class modifyResident : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Residents",
                newName: "Purok");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Purok",
                table: "Residents",
                newName: "Address");
        }
    }
}
