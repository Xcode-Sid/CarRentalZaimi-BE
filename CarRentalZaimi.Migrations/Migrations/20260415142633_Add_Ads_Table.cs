using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalZaimi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Add_Ads_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: true),
                    ImageName = table.Column<string>(type: "longtext", nullable: true),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: true),
                    VideoName = table.Column<string>(type: "longtext", nullable: true),
                    VideoUrl = table.Column<string>(type: "longtext", nullable: true),
                    LinkUrl = table.Column<string>(type: "longtext", nullable: true),
                    Position = table.Column<string>(type: "longtext", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: true),
                    CreatedIP = table.Column<string>(type: "longtext", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "char(36)", nullable: true),
                    DeletedIP = table.Column<string>(type: "longtext", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "char(36)", nullable: true),
                    ModifiedIP = table.Column<string>(type: "longtext", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ads");

        }
    }
}
