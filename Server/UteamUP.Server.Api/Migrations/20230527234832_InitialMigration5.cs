using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UteamUP.Server.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Locations_LocationId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_LocationId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "LocationTag",
                columns: table => new
                {
                    LocationsId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationTag", x => new { x.LocationsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_LocationTag_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationTag_TagsId",
                table: "LocationTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationTag");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Tags",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_LocationId",
                table: "Tags",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Locations_LocationId",
                table: "Tags",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
