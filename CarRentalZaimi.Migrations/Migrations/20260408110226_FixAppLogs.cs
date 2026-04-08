using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalZaimi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class FixAppLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "AppLogs",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "MessageTemplate",
                table: "AppLogs",
                newName: "Template");

            migrationBuilder.AlterColumn<string>(
                name: "Timestamp",
                table: "AppLogs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "AppLogs",
                newName: "TimeStamp");

            migrationBuilder.RenameColumn(
                name: "Template",
                table: "AppLogs",
                newName: "MessageTemplate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "AppLogs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }
    }
}
