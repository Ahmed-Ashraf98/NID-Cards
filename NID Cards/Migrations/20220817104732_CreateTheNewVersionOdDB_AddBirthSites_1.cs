using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NID_Cards.Migrations
{
    public partial class CreateTheNewVersionOdDB_AddBirthSites_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BirthSites",
                columns: table => new
                {
                    BirthSiteID = table.Column<byte>(type: "tinyint", nullable: false),
                    BirthSiteName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirthSites", x => x.BirthSiteID);
                });

            migrationBuilder.CreateTable(
                name: "Governorates",
                columns: table => new
                {
                    GovernorateID = table.Column<byte>(type: "tinyint", nullable: false),
                    GovernorateName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorates", x => x.GovernorateID);
                });

            migrationBuilder.CreateTable(
                name: "Citizens",
                columns: table => new
                {
                    NID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceOfIssue = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Religion = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    HusbandName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NIDIsActive = table.Column<bool>(type: "bit", nullable: false),
                    PersonalPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    NIDFrontImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    NIDBackImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    GovernorateID = table.Column<byte>(type: "tinyint", nullable: false),
                    BirthSiteID = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.NID);
                    table.ForeignKey(
                        name: "FK_Citizens_BirthSites_BirthSiteID",
                        column: x => x.BirthSiteID,
                        principalTable: "BirthSites",
                        principalColumn: "BirthSiteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Citizens_Governorates_GovernorateID",
                        column: x => x.GovernorateID,
                        principalTable: "Governorates",
                        principalColumn: "GovernorateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_BirthSiteID",
                table: "Citizens",
                column: "BirthSiteID");

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_GovernorateID",
                table: "Citizens",
                column: "GovernorateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citizens");

            migrationBuilder.DropTable(
                name: "BirthSites");

            migrationBuilder.DropTable(
                name: "Governorates");
        }
    }
}
