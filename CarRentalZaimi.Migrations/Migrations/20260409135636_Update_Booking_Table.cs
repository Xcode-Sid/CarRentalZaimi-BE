using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalZaimi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Update_Booking_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "BookingServices");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Bookings",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Bookings",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefuzedBy",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefuzedReason",
                table: "Bookings",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RefuzedBy",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RefuzedReason",
                table: "Bookings");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerDay",
                table: "BookingServices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
