using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SecuritySite.Migrations
{
    /// <inheritdoc />
    public partial class features : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountAddons");

            migrationBuilder.AddColumn<List<string>>(
                name: "Features",
                table: "MonitoredAccounts",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Features",
                table: "MonitoredAccounts");

            migrationBuilder.CreateTable(
                name: "AccountAddons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BasePlan = table.Column<bool>(type: "boolean", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Commercial = table.Column<bool>(type: "boolean", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    FeatureName = table.Column<string>(type: "text", nullable: false),
                    MonitoredAccountId = table.Column<int>(type: "integer", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAddons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountAddons_MonitoredAccounts_MonitoredAccountId",
                        column: x => x.MonitoredAccountId,
                        principalTable: "MonitoredAccounts",
                        principalColumn: "MonitoredAccountId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountAddons_MonitoredAccountId",
                table: "AccountAddons",
                column: "MonitoredAccountId");
        }
    }
}
