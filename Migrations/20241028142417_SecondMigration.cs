using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BrgyLink.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13b5a58b-2538-4d1d-a859-93f14bbb0046", null, "admin", "admin" },
                    { "82b05f32-b0e0-4665-bc9e-a998aa86763c", null, "client", "client" },
                    { "e6def268-a0de-4b1e-9f31-b49b04f08277", null, "resident", "resident" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13b5a58b-2538-4d1d-a859-93f14bbb0046");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82b05f32-b0e0-4665-bc9e-a998aa86763c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6def268-a0de-4b1e-9f31-b49b04f08277");
        }
    }
}
