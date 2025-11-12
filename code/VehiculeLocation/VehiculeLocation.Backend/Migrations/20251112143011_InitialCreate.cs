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
                name: "Vehicules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Modele = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Place = table.Column<int>(type: "INTEGER", nullable: false),
                    DailyLocationPrice = table.Column<float>(type: "REAL", nullable: false),
                    Motorisation = table.Column<string>(type: "TEXT", nullable: false),
                    AutomaticTransmission = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateStart = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    VehiculeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Vehicules_VehiculeId",
                        column: x => x.VehiculeId,
                        principalTable: "Vehicules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Vehicules",
                columns: new[] { "Id", "AutomaticTransmission", "DailyLocationPrice", "ImagePath", "Modele", "Motorisation", "Place" },
                values: new object[,]
                {
                    { 1, false, 30.5f, "https://lh6.googleusercontent.com/proxy/HEmJ7BvDYuo0uaBAecqWSianL5MBS7XzuqV7Nu-scTwvXRipC-hl4cd9crmQyWRGlpVFfhzCUNeDJr5najGvzJgFieuCF71nqwFxy22zmJpFOaGjHaieImL9dQCHvliO5n5Z8AgF83m7qs4ntaPlLsE", "Renault Clio V", "Essence", 5 },
                    { 2, true, 65f, "https://lh6.googleusercontent.com/proxy/HEmJ7BvDYuo0uaBAecqWSianL5MBS7XzuqV7Nu-scTwvXRipC-hl4cd9crmQyWRGlpVFfhzCUNeDJr5najGvzJgFieuCF71nqwFxy22zmJpFOaGjHaieImL9dQCHvliO5n5Z8AgF83m7qs4ntaPlLsE", "Peugeot 3008", "Diesel", 5 },
                    { 3, true, 25f, "https://lh6.googleusercontent.com/proxy/HEmJ7BvDYuo0uaBAecqWSianL5MBS7XzuqV7Nu-scTwvXRipC-hl4cd9crmQyWRGlpVFfhzCUNeDJr5najGvzJgFieuCF71nqwFxy22zmJpFOaGjHaieImL9dQCHvliO5n5Z8AgF83m7qs4ntaPlLsE", "Renault Twingo", "Electrique", 5 },
                    { 4, false, 35f, "https://lh6.googleusercontent.com/proxy/HEmJ7BvDYuo0uaBAecqWSianL5MBS7XzuqV7Nu-scTwvXRipC-hl4cd9crmQyWRGlpVFfhzCUNeDJr5najGvzJgFieuCF71nqwFxy22zmJpFOaGjHaieImL9dQCHvliO5n5Z8AgF83m7qs4ntaPlLsE", "Citroën C3", "Essence", 5 }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "DateEnd", "DateStart", "VehiculeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 13, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 11, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2025, 12, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, new DateTime(2025, 12, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, new DateTime(2025, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_VehiculeId",
                table: "Locations",
                column: "VehiculeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Vehicules");
        }
    }
}
