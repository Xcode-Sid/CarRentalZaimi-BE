using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalZaimi.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Update_Cars_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdaptiveCruiseControl",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AndroidAuto",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AppleCarPlay",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Bluetooth",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Camera",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ClimateControl",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CruiseControl",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LEDHeadlights",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LaneDepartureAlert",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PanoramicRoof",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ParkingSensors",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ThirdRowSeats",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ToyotaSafetySense",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WirelessCharging",
                table: "Cars",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdaptiveCruiseControl",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "AndroidAuto",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "AppleCarPlay",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Bluetooth",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Camera",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ClimateControl",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CruiseControl",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "LEDHeadlights",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "LaneDepartureAlert",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "PanoramicRoof",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ParkingSensors",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ThirdRowSeats",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ToyotaSafetySense",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "WirelessCharging",
                table: "Cars");
        }
    }
}
