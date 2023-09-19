using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecuritySite.Migrations
{
    /// <inheritdoc />
    public partial class CertRequestChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "accepted",
                table: "MonitoredAccounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "CertificateRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accepted",
                table: "MonitoredAccounts");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "CertificateRequests");
        }
    }
}
