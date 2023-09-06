using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecuritySite.Migrations
{
    /// <inheritdoc />
    public partial class NewLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "completed",
                table: "MonitoredAccounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "completed",
                table: "MonitoredAccounts");
        }
    }
}
