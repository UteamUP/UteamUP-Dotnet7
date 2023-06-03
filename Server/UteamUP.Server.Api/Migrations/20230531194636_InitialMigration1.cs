using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UteamUP.Server.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationTags",
                table: "LocationTags");

            migrationBuilder.DropIndex(
                name: "IX_LocationTags_LocationId",
                table: "LocationTags");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "LocationTags",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationTags",
                table: "LocationTags",
                columns: new[] { "LocationId", "TagId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationTags",
                table: "LocationTags");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "LocationTags",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationTags",
                table: "LocationTags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LocationTags_LocationId",
                table: "LocationTags",
                column: "LocationId");
        }
    }
}
