using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecuritySite.Migrations
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificateRequests_MonitoredAccounts_AccountMonitoredAccou~",
                table: "CertificateRequests");

            migrationBuilder.DropIndex(
                name: "IX_CertificateRequests_AccountMonitoredAccountId",
                table: "CertificateRequests");

            migrationBuilder.DropColumn(
                name: "AccountMonitoredAccountId",
                table: "CertificateRequests");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "AccountGUID");

            migrationBuilder.AddColumn<string>(
                name: "AccountOwner",
                table: "MonitoredAccounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountOwner",
                table: "CertificateRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "CertificateRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Submitted",
                table: "CertificateRequests",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountOwner",
                table: "MonitoredAccounts");

            migrationBuilder.DropColumn(
                name: "AccountOwner",
                table: "CertificateRequests");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "CertificateRequests");

            migrationBuilder.DropColumn(
                name: "Submitted",
                table: "CertificateRequests");

            migrationBuilder.RenameColumn(
                name: "AccountGUID",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<int>(
                name: "AccountMonitoredAccountId",
                table: "CertificateRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateRequests_AccountMonitoredAccountId",
                table: "CertificateRequests",
                column: "AccountMonitoredAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificateRequests_MonitoredAccounts_AccountMonitoredAccou~",
                table: "CertificateRequests",
                column: "AccountMonitoredAccountId",
                principalTable: "MonitoredAccounts",
                principalColumn: "MonitoredAccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
