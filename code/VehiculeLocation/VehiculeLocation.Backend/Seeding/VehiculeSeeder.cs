using VehiculeLocation.Backend.Models;
using System.Collections.Generic;

namespace VehiculeLocation.Backend.Data.Seeding
{
    /// <summary>
    /// Génère les données de la table Vehicule
    /// </summary>
    public static class VehiculeSeeder
    {
        // Cette méthode statique renvoie une liste de véhicules à insérer
        public static IEnumerable<Vehicule> GetVehiculeSeedData()
        {
            return new List<Vehicule>
            {
                new Vehicule
                {
                    Id = 1,
                    Modele = "Renault Clio V",
                    Place = 5,
                    DailyLocationPrice = 30.50f,
                    Motorisation = TypeMotorisationEnum.Essence,
                    AutomaticTransmission = false,
                    ImagePath = "https://lh6.googleusercontent.com/proxy/HEmJ7BvDYuo0uaBAecqWSianL5MBS7XzuqV7Nu-scTwvXRipC-hl4cd9crmQyWRGlpVFfhzCUNeDJr5najGvzJgFieuCF71nqwFxy22zmJpFOaGjHaieImL9dQCHvliO5n5Z8AgF83m7qs4ntaPlLsE"
                },
                new Vehicule
                {
                    Id = 2,
                    Modele = "Peugeot 3008",
                    Place = 5,
                    DailyLocationPrice = 65.00f,
                    Motorisation = TypeMotorisationEnum.Diesel,
                    AutomaticTransmission = true,
                    ImagePath = "https://lh6.googleusercontent.com/proxy/HEmJ7BvDYuo0uaBAecqWSianL5MBS7XzuqV7Nu-scTwvXRipC-hl4cd9crmQyWRGlpVFfhzCUNeDJr5najGvzJgFieuCF71nqwFxy22zmJpFOaGjHaieImL9dQCHvliO5n5Z8AgF83m7qs4ntaPlLsE"
                },
                new Vehicule
                {
                    Id = 3,
                    Modele = "Renault Twingo",
                    Place = 5,
                    DailyLocationPrice = 25.00f,
                    Motorisation = TypeMotorisationEnum.Electrique,
                    AutomaticTransmission = true,
                    ImagePath = "https://lh6.googleusercontent.com/proxy/HEmJ7BvDYuo0uaBAecqWSianL5MBS7XzuqV7Nu-scTwvXRipC-hl4cd9crmQyWRGlpVFfhzCUNeDJr5najGvzJgFieuCF71nqwFxy22zmJpFOaGjHaieImL9dQCHvliO5n5Z8AgF83m7qs4ntaPlLsE"
                },
                new Vehicule
                {
                    Id = 4,
                    Modele = "Citroën C3",
                    Place = 5,
                    DailyLocationPrice = 35.00f,
                    Motorisation = TypeMotorisationEnum.Essence,
                    AutomaticTransmission = false,
                    ImagePath = "https://lh6.googleusercontent.com/proxy/HEmJ7BvDYuo0uaBAecqWSianL5MBS7XzuqV7Nu-scTwvXRipC-hl4cd9crmQyWRGlpVFfhzCUNeDJr5najGvzJgFieuCF71nqwFxy22zmJpFOaGjHaieImL9dQCHvliO5n5Z8AgF83m7qs4ntaPlLsE"
                }
            };
        }
    }
}