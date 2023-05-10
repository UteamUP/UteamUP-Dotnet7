using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UteamUP.Server.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedLogAndUpdatedTagAndCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Parts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Assets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: false),
                    CreatorId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatorId",
                table: "Categories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_CreatorId",
                table: "Assets",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_CreatorId",
                table: "Logs",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Users_CreatorId",
                table: "Assets",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_CreatorId",
                table: "Categories",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Users_CreatorId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_CreatorId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatorId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Assets_CreatorId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Assets");
        }
    }
}
