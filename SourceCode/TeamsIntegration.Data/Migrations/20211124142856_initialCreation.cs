using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamsIntegration.Data.Migrations
{
    public partial class initialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamAppsInstallation",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsInstalled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamAppsInstallation", x => new { x.TeamId, x.AppId });
                });

            migrationBuilder.CreateTable(
                name: "UnMappedClasses",
                columns: table => new
                {
                    ExternalClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tenants = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastCheckedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnMappedClasses", x => x.ExternalClassId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamAppsInstallation");

            migrationBuilder.DropTable(
                name: "UnMappedClasses");
        }
    }
}
