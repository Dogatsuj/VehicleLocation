using VehiculeLocation.Backend.Models;
using System.Collections.Generic;

namespace VehiculeLocation.Backend.Data.Seeding
{
    /// <summary>
    /// Génère les données de la table Vehicule
    /// </summary>
    public static class VehicleSeeder
    {
        public static IEnumerable<Vehicle> GetVehiculeSeedData()
        {
            return new List<Vehicle>
            {
                new Vehicle
                {
                    Id = 1,
                    Brand = "Renault",
                    Model = "Clio V",
                    Seats = 5,
                    DailyRentalPrice = 30.50f,
                    Motorisation = TypeMotorisationEnum.Petrol,
                    IsAutomaticTransmission = false,
                    Description = "Une voiture qui roule",
                    ImagePath = "https://upload.wikimedia.org/wikipedia/commons/6/6d/Dunkerque-1.jpg",
                    UserId = 1
                },
                new Vehicle
                {
                    Id = 2,
                    Brand = "Peugeot",
                    Model = "3008",
                    Seats = 5,
                    DailyRentalPrice = 65.00f,
                    Motorisation = TypeMotorisationEnum.Diesel,
                    IsAutomaticTransmission = true,
                    Description = "Une voiture qui roule",
                    ImagePath = "https://upload.wikimedia.org/wikipedia/commons/6/6d/Dunkerque-1.jpg",
                    UserId = 1
                },
                new Vehicle
                {
                    Id = 3,
                    Brand = "Renault",
                    Model = "Twingo",
                    Seats = 5,
                    DailyRentalPrice = 25.00f,
                    Motorisation = TypeMotorisationEnum.Electric,
                    IsAutomaticTransmission = true,
                    Description = "Une voiture qui roule",
                    ImagePath = "https://upload.wikimedia.org/wikipedia/commons/6/6d/Dunkerque-1.jpg",
                    UserId = 1
                },
                new Vehicle
                {
                    Id = 4,
                    Brand = "Citroën",
                    Model = "C3",
                    Seats = 5,
                    DailyRentalPrice = 35.00f,
                    Motorisation = TypeMotorisationEnum.Petrol,
                    IsAutomaticTransmission = false,
                    Description = "Une voiture qui roule",
                    ImagePath = "https://upload.wikimedia.org/wikipedia/commons/6/6d/Dunkerque-1.jpg",
                    UserId = 1
                }
            };
        }
    }
}