using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VehiculeLocation.Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Seats = table.Column<int>(type: "INTEGER", nullable: false),
                    DailyRentalPrice = table.Column<float>(type: "REAL", nullable: false),
                    Motorisation = table.Column<string>(type: "TEXT", nullable: false),
                    IsAutomaticTransmission = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    VehicleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Brand", "DailyRentalPrice", "Description", "ImagePath", "IsAutomaticTransmission", "Model", "Motorisation", "Seats" },
                values: new object[,]
                {
                    { 1, "Renault", 30.5f, "Une voiture qui roule", "https://upload.wikimedia.org/wikipedia/commons/6/6d/Dunkerque-1.jpg", false, "Clio V", "Petrol", 5 },
                    { 2, "Peugeot", 65f, "Une voiture qui roule", "https://upload.wikimedia.org/wikipedia/commons/6/6d/Dunkerque-1.jpg", true, "3008", "Diesel", 5 },
                    { 3, "Renault", 25f, "Une voiture qui roule", "https://upload.wikimedia.org/wikipedia/commons/6/6d/Dunkerque-1.jpg", true, "Twingo", "Electric", 5 },
                    { 4, "Citroën", 35f, "Une voiture qui roule", "https://upload.wikimedia.org/wikipedia/commons/6/6d/Dunkerque-1.jpg", false, "C3", "Petrol", 5 }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateEnd", "DateStart", "VehicleId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 13, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 11, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2025, 12, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, new DateTime(2025, 12, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, new DateTime(2025, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_VehicleId",
                table: "Locations",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
