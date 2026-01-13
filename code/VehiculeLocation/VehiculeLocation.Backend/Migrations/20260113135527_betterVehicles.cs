using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiculeLocation.Backend.Migrations
{
    /// <inheritdoc />
    public partial class betterVehicles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImagePath" },
                values: new object[] { "Une voiture qui roule assez bien.", "https://s3-eu-west-1.amazonaws.com/staticeu.izmocars.com/toolkit/commonassets/2024/24renault/24renaultcliotechnohb5rb/24renaultcliotechnohb5rb_animations/colorpix/fr/400x300/renault_24cliotechnohb5rb_orangevalencia_angular-front.webp" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImagePath" },
                values: new object[] { "Une voiture qui roule vraiment bien, vous allez voir c'est top !", "https://www.topgear.com/sites/default/files/2024/09/PEUGEOT_3008_EXT_13.jpg" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagePath",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/9/92/1997_Renault_Twingo_1.15_%281%29.jpg/330px-1997_Renault_Twingo_1.15_%281%29.jpg");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImagePath" },
                values: new object[] { "Une voiture qui roule très bien", "https://images.caradisiac.com/logos/5/8/1/4/135814/S8-Citroen-C-40397.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImagePath" },
                values: new object[] { "Une voiture qui roule", "https://upload.wikimedia.org/wikipedia/commons/6/6d/Dunkerque-1.jpg" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImagePath" },
                values: new object[] { "Une voiture qui roule", "https://upload.wikimedia.org/wikipedia/commons/6/6d/Dunkerque-1.jpg" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagePath",
                value: "https://upload.wikimedia.org/wikipedia/commons/6/6d/Dunkerque-1.jpg");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImagePath" },
                values: new object[] { "Une voiture qui roule", "https://upload.wikimedia.org/wikipedia/commons/6/6d/Dunkerque-1.jpg" });
        }
    }
}
