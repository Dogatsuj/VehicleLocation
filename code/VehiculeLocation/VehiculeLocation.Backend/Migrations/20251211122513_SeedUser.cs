using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VehiculeLocation.Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "IsAdmin", "Password", "Username" },
                values: new object[,]
                {
                    { 1, true, "$2a$12$.SP2dh9GQr/nVpFqTd.XGudDM/FkyBE8gKoXQmmTmE08tejW6SOb6", "admin" },
                    { 2, true, "$2a$12$.SP2dh9GQr/nVpFqTd.XGudDM/FkyBE8gKoXQmmTmE08tejW6SOb6", "Grigory Ivanovich Kulik" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
