using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UteamUP.Server.Api.Migrations
{
    /// <inheritdoc />
    public partial class StockTagAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Tenants_TenantId",
                table: "Stocks");

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Stocks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Stocks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StockTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StockId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTags_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_LocationId",
                table: "Stocks",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTags_StockId_TagId",
                table: "StockTags",
                columns: new[] { "StockId", "TagId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockTags_TagId",
                table: "StockTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Locations_LocationId",
                table: "Stocks",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Tenants_TenantId",
                table: "Stocks",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Locations_LocationId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Tenants_TenantId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "StockTags");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_LocationId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Stocks");

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Stocks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Tenants_TenantId",
                table: "Stocks",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");
        }
    }
}
