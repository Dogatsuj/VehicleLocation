using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiculeLocation.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AjoutInfoVehicule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Modele",
                table: "Vehicules",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AutomaticTransmission",
                table: "Vehicules",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "DailyLocationPrice",
                table: "Vehicules",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Motorisation",
                table: "Vehicules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Vehicules",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AutomaticTransmission", "DailyLocationPrice", "Motorisation" },
                values: new object[] { false, 30.5f, "Essence" });

            migrationBuilder.UpdateData(
                table: "Vehicules",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AutomaticTransmission", "DailyLocationPrice", "Motorisation" },
                values: new object[] { true, 65f, "Diesel" });

            migrationBuilder.UpdateData(
                table: "Vehicules",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AutomaticTransmission", "DailyLocationPrice", "Motorisation" },
                values: new object[] { true, 25f, "Electrique" });

            migrationBuilder.UpdateData(
                table: "Vehicules",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AutomaticTransmission", "DailyLocationPrice", "Motorisation" },
                values: new object[] { false, 35f, "Essence" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutomaticTransmission",
                table: "Vehicules");

            migrationBuilder.DropColumn(
                name: "DailyLocationPrice",
                table: "Vehicules");

            migrationBuilder.DropColumn(
                name: "Motorisation",
                table: "Vehicules");

            migrationBuilder.AlterColumn<string>(
                name: "Modele",
                table: "Vehicules",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);
        }
    }
}
