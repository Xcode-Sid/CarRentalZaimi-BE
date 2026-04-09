using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalZaimi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsRecomandedColum_CarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRecommended",
                table: "Cars",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecommended",
                table: "Cars");
        }
    }
}
