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
            migrationBuilder.DropIndex(
                name: "IX_Tags_Name",
                table: "Tags");

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
                name: "IX_LocationTags_LocationId_TagId",
                table: "LocationTags",
                columns: new[] { "LocationId", "TagId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationTags",
                table: "LocationTags");

            migrationBuilder.DropIndex(
                name: "IX_LocationTags_LocationId_TagId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);
        }
    }
}
