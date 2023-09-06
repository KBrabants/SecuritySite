using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SecuritySite.Migrations
{
    /// <inheritdoc />
    public partial class AccountAddons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountFeatures_MonitoredAccounts_MonitoredAccountId",
                table: "AccountFeatures");

            migrationBuilder.DropIndex(
                name: "IX_AccountFeatures_MonitoredAccountId",
                table: "AccountFeatures");

            migrationBuilder.DropColumn(
                name: "MonthlyCost",
                table: "MonitoredAccounts");

            migrationBuilder.DropColumn(
                name: "accepted",
                table: "MonitoredAccounts");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "AccountFeatures");

            migrationBuilder.DropColumn(
                name: "MonitoredAccountId",
                table: "AccountFeatures");

            migrationBuilder.AlterColumn<string>(
                name: "zipcode",
                table: "MonitoredAccounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "state",
                table: "MonitoredAccounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "locationName",
                table: "MonitoredAccounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "emergencyContactPhoneNumber",
                table: "MonitoredAccounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "emergencyContact",
                table: "MonitoredAccounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "durressCode",
                table: "MonitoredAccounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "county",
                table: "MonitoredAccounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "city",
                table: "MonitoredAccounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "alarmPassword",
                table: "MonitoredAccounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "MonitoredAccounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "BasePlan",
                table: "AccountFeatures",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "AccountFeatures",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AccountAddons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    FeatureName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Commercial = table.Column<bool>(type: "boolean", nullable: false),
                    BasePlan = table.Column<bool>(type: "boolean", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MonitoredAccountId = table.Column<int>(type: "integer", nullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountAddons");

            migrationBuilder.DropColumn(
                name: "BasePlan",
                table: "AccountFeatures");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "AccountFeatures");

            migrationBuilder.AlterColumn<string>(
                name: "zipcode",
                table: "MonitoredAccounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "state",
                table: "MonitoredAccounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "locationName",
                table: "MonitoredAccounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "emergencyContactPhoneNumber",
                table: "MonitoredAccounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "emergencyContact",
                table: "MonitoredAccounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "durressCode",
                table: "MonitoredAccounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "county",
                table: "MonitoredAccounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "city",
                table: "MonitoredAccounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "alarmPassword",
                table: "MonitoredAccounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "MonitoredAccounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<float>(
                name: "MonthlyCost",
                table: "MonitoredAccounts",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "accepted",
                table: "MonitoredAccounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "AccountFeatures",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MonitoredAccountId",
                table: "AccountFeatures",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountFeatures_MonitoredAccountId",
                table: "AccountFeatures",
                column: "MonitoredAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountFeatures_MonitoredAccounts_MonitoredAccountId",
                table: "AccountFeatures",
                column: "MonitoredAccountId",
                principalTable: "MonitoredAccounts",
                principalColumn: "MonitoredAccountId");
        }
    }
}
