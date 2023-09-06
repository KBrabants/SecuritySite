using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecuritySite.Migrations
{
    /// <inheritdoc />
    public partial class accounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MonthlyCost",
                table: "MonitoredAccounts",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "commercial",
                table: "MonitoredAccounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyCost",
                table: "MonitoredAccounts");

            migrationBuilder.DropColumn(
                name: "commercial",
                table: "MonitoredAccounts");
        }
    }
}
