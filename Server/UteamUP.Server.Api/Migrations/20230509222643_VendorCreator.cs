using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UteamUP.Server.Api.Migrations
{
    /// <inheritdoc />
    public partial class VendorCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Vendor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CreatorId",
                table: "Vendor",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Users_CreatorId",
                table: "Vendor",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Users_CreatorId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_CreatorId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Vendor");
        }
    }
}
