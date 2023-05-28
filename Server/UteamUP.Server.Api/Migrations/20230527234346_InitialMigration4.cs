using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UteamUP.Server.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Assets_AssetId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Stocks_StockId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_AssetId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_StockId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "Locations");

            migrationBuilder.CreateTable(
                name: "AssetLocation",
                columns: table => new
                {
                    AssetsId = table.Column<int>(type: "integer", nullable: false),
                    LocationsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetLocation", x => new { x.AssetsId, x.LocationsId });
                    table.ForeignKey(
                        name: "FK_AssetLocation_Assets_AssetsId",
                        column: x => x.AssetsId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetLocation_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationStock",
                columns: table => new
                {
                    LocationsId = table.Column<int>(type: "integer", nullable: false),
                    StocksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationStock", x => new { x.LocationsId, x.StocksId });
                    table.ForeignKey(
                        name: "FK_LocationStock_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationStock_Stocks_StocksId",
                        column: x => x.StocksId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetLocation_LocationsId",
                table: "AssetLocation",
                column: "LocationsId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationStock_StocksId",
                table: "LocationStock",
                column: "StocksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetLocation");

            migrationBuilder.DropTable(
                name: "LocationStock");

            migrationBuilder.AddColumn<int>(
                name: "AssetId",
                table: "Locations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "Locations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AssetId",
                table: "Locations",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_StockId",
                table: "Locations",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Assets_AssetId",
                table: "Locations",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Stocks_StockId",
                table: "Locations",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id");
        }
    }
}
