using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalZaimi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Delete_Contact_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "ContactMessages");

            migrationBuilder.DropColumn(
                name: "ReadAt",
                table: "ContactMessages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "ContactMessages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadAt",
                table: "ContactMessages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: true),
                    CreatedIP = table.Column<string>(type: "longtext", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedIP = table.Column<string>(type: "longtext", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    FullName = table.Column<string>(type: "longtext", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Message = table.Column<string>(type: "longtext", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "char(36)", nullable: true),
                    ModifiedIP = table.Column<string>(type: "longtext", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Phone = table.Column<string>(type: "longtext", nullable: true),
                    Subject = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }
    }
}
