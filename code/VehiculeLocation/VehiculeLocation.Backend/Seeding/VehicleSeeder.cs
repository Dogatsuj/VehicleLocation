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
                    Description = "Une voiture qui roule assez bien.",
                    ImagePath = "https://s3-eu-west-1.amazonaws.com/staticeu.izmocars.com/toolkit/commonassets/2024/24renault/24renaultcliotechnohb5rb/24renaultcliotechnohb5rb_animations/colorpix/fr/400x300/renault_24cliotechnohb5rb_orangevalencia_angular-front.webp",
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
                    Description = "Une voiture qui roule vraiment bien, vous allez voir c'est top !",
                    ImagePath = "https://www.topgear.com/sites/default/files/2024/09/PEUGEOT_3008_EXT_13.jpg",
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
                    ImagePath = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/92/1997_Renault_Twingo_1.15_%281%29.jpg/330px-1997_Renault_Twingo_1.15_%281%29.jpg",
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
                    Description = "Une voiture qui roule très bien",
                    ImagePath = "https://images.caradisiac.com/logos/5/8/1/4/135814/S8-Citroen-C-40397.jpg",
                    UserId = 1
                }
            };
        }
    }
}