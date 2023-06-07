using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UteamUP.Server.Api.Migrations
{
    /// <inheritdoc />
    public partial class VendorUpdatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_CreatorId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Vendor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Categories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_UpdatedById",
                table: "Vendor",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_CreatorId",
                table: "Categories",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Users_UpdatedById",
                table: "Vendor",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_CreatorId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Users_UpdatedById",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_UpdatedById",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Vendor");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_CreatorId",
                table: "Categories",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
