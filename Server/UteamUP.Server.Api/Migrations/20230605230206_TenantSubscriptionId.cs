using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UteamUP.Server.Api.Migrations
{
    /// <inheritdoc />
    public partial class TenantSubscriptionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "Tenants",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Tenants");
        }
    }
}
