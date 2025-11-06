using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiculeLocation.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AjoutVehicleImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Vehicules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Vehicules",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "https://lh6.googleusercontent.com/proxy/HEmJ7BvDYuo0uaBAecqWSianL5MBS7XzuqV7Nu-scTwvXRipC-hl4cd9crmQyWRGlpVFfhzCUNeDJr5najGvzJgFieuCF71nqwFxy22zmJpFOaGjHaieImL9dQCHvliO5n5Z8AgF83m7qs4ntaPlLsE");

            migrationBuilder.UpdateData(
                table: "Vehicules",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagePath",
                value: "https://lh6.googleusercontent.com/proxy/HEmJ7BvDYuo0uaBAecqWSianL5MBS7XzuqV7Nu-scTwvXRipC-hl4cd9crmQyWRGlpVFfhzCUNeDJr5najGvzJgFieuCF71nqwFxy22zmJpFOaGjHaieImL9dQCHvliO5n5Z8AgF83m7qs4ntaPlLsE");

            migrationBuilder.UpdateData(
                table: "Vehicules",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagePath",
                value: "https://lh6.googleusercontent.com/proxy/HEmJ7BvDYuo0uaBAecqWSianL5MBS7XzuqV7Nu-scTwvXRipC-hl4cd9crmQyWRGlpVFfhzCUNeDJr5najGvzJgFieuCF71nqwFxy22zmJpFOaGjHaieImL9dQCHvliO5n5Z8AgF83m7qs4ntaPlLsE");

            migrationBuilder.UpdateData(
                table: "Vehicules",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagePath",
                value: "https://lh6.googleusercontent.com/proxy/HEmJ7BvDYuo0uaBAecqWSianL5MBS7XzuqV7Nu-scTwvXRipC-hl4cd9crmQyWRGlpVFfhzCUNeDJr5najGvzJgFieuCF71nqwFxy22zmJpFOaGjHaieImL9dQCHvliO5n5Z8AgF83m7qs4ntaPlLsE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Vehicules");
        }
    }
}
