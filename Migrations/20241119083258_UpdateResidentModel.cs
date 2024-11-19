using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrgyLink.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResidentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarangayOfficials_Residents_ResidentID",
                table: "BarangayOfficials");

            migrationBuilder.DropIndex(
                name: "IX_BarangayOfficials_ResidentID",
                table: "BarangayOfficials");

            migrationBuilder.DropColumn(
                name: "ResidentID",
                table: "BarangayOfficials");

            migrationBuilder.AlterColumn<string>(
                name: "VoterStatus",
                table: "Residents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Non-voter",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ResidencyStatus",
                table: "Residents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Resident",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Residents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Filipino",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateRegistered",
                table: "Residents",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CivilStatus",
                table: "Residents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Single",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VoterStatus",
                table: "Residents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "Non-voter");

            migrationBuilder.AlterColumn<string>(
                name: "ResidencyStatus",
                table: "Residents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "Resident");

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Residents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Filipino");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateRegistered",
                table: "Residents",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "CivilStatus",
                table: "Residents",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "Single");

            migrationBuilder.AddColumn<int>(
                name: "ResidentID",
                table: "BarangayOfficials",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BarangayOfficials_ResidentID",
                table: "BarangayOfficials",
                column: "ResidentID");

            migrationBuilder.AddForeignKey(
                name: "FK_BarangayOfficials_Residents_ResidentID",
                table: "BarangayOfficials",
                column: "ResidentID",
                principalTable: "Residents",
                principalColumn: "ResidentID");
        }
    }
}
