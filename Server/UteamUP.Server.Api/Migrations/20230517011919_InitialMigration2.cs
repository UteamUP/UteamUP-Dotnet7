using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UteamUP.Server.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkorderId",
                table: "Parts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    UrlPath = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ReportId",
                table: "Users",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_WorkorderId",
                table: "Parts",
                column: "WorkorderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Workorders_WorkorderId",
                table: "Parts",
                column: "WorkorderId",
                principalTable: "Workorders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Reports_ReportId",
                table: "Users",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Workorders_WorkorderId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Reports_ReportId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Users_ReportId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Parts_WorkorderId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkorderId",
                table: "Parts");
        }
    }
}
