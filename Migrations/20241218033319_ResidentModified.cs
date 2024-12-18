using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrgyLink.Migrations
{
    /// <inheritdoc />
    public partial class ResidentModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfRegisteredPeople",
                table: "Puroks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfRegisteredPeople",
                table: "Puroks");
        }
    }
}
