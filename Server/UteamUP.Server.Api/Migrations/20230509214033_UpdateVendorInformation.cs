using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UteamUP.Server.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVendorInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Categories_CategoryId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_CategoryId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "OpeningHoursFrom",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "OpeningHoursTo",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "VendorType",
                table: "Vendor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Vendor",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "HourlyRate",
                table: "Vendor",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "OpeningHoursFrom",
                table: "Vendor",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OpeningHoursTo",
                table: "Vendor",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VendorType",
                table: "Vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CategoryId",
                table: "Vendor",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Categories_CategoryId",
                table: "Vendor",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
