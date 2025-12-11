using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiculeLocation.Backend.Migrations
{
    /// <inheritdoc />
    public partial class FixRegularUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsAdmin", "Password" },
                values: new object[] { false, "$2a$12$ztnvLsPVjDZ7dDq2y.K4seU6x47uk/CinTHGN.OaY3rwrSGpVqEDC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsAdmin", "Password" },
                values: new object[] { true, "$2a$12$.SP2dh9GQr/nVpFqTd.XGudDM/FkyBE8gKoXQmmTmE08tejW6SOb6" });
        }
    }
}
