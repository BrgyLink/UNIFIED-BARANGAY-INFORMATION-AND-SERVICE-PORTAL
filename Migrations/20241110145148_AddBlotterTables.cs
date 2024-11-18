using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrgyLink.Migrations
{
    /// <inheritdoc />
    public partial class AddBlotterTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blotter",
                columns: table => new
                {
                    BlotterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplainantID = table.Column<int>(type: "int", nullable: false),
                    RespondentID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DateFiled = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blotter", x => x.BlotterID);
                    table.ForeignKey(
                        name: "FK_Blotter_Residents_ComplainantID",
                        column: x => x.ComplainantID,
                        principalTable: "Residents",
                        principalColumn: "ResidentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blotter_Residents_RespondentID",
                        column: x => x.RespondentID,
                        principalTable: "Residents",
                        principalColumn: "ResidentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlotterVictims",
                columns: table => new
                {
                    BlotterVictimID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlotterID = table.Column<int>(type: "int", nullable: false),
                    ResidentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlotterVictims", x => x.BlotterVictimID);
                    table.ForeignKey(
                        name: "FK_BlotterVictims_Blotter_BlotterID",
                        column: x => x.BlotterID,
                        principalTable: "Blotter",
                        principalColumn: "BlotterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlotterVictims_Residents_ResidentID",
                        column: x => x.ResidentID,
                        principalTable: "Residents",
                        principalColumn: "ResidentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blotter_ComplainantID",
                table: "Blotter",
                column: "ComplainantID");

            migrationBuilder.CreateIndex(
                name: "IX_Blotter_RespondentID",
                table: "Blotter",
                column: "RespondentID");

            migrationBuilder.CreateIndex(
                name: "IX_BlotterVictims_BlotterID",
                table: "BlotterVictims",
                column: "BlotterID");

            migrationBuilder.CreateIndex(
                name: "IX_BlotterVictims_ResidentID",
                table: "BlotterVictims",
                column: "ResidentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlotterVictims");

            migrationBuilder.DropTable(
                name: "Blotter");
        }
    }
}
