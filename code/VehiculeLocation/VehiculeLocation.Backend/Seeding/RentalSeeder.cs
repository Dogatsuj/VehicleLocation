using VehiculeLocation.Backend.Models;
using System.Collections.Generic;
using System;

namespace VehiculeLocation.Backend.Data.Seeding
{
    public static class RentalSeeder
    {
        /// <summary>
        /// Ajout des entités dans la table Location
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Rental> GetLocationSeedData()
        {
            // Définir une date de référence stable pour le seeding
            var dateBase = new DateTime(2025, 12, 10);

            return new List<Rental>
            {
                // Location 1 : Véhicule 1 (Clio V) - 2 jours
                new Rental
                {
                    Id = 1,
                    VehicleId = 1,
                    DateStart = dateBase.AddDays(1).AddHours(9), // 11/12/2025 à 9h
                    DateEnd = dateBase.AddDays(3).AddHours(18),  // 13/12/2025 à 18h
                    UserId = 2
                },
                
                // Location 2 : Véhicule 2 (3008) - 1 journée
                new Rental
                {
                    Id = 2,
                    VehicleId = 2,
                    DateStart = dateBase.AddDays(5).AddHours(14), // 15/12/2025 à 14h
                    DateEnd = dateBase.AddDays(6).AddHours(14),   // 16/12/2025 à 14h
                    UserId = 2
                },
                
                // Location 3 : Véhicule 3 (Twingo) - Courte durée
                new Rental
                {
                    Id = 3,
                    VehicleId = 3,
                    DateStart = dateBase.AddDays(2).AddHours(8), // 12/12/2025 à 8h
                    DateEnd = dateBase.AddDays(2).AddHours(12),  // 12/12/2025 à 12h
                    UserId = 2
                },
                
                // Location 4 : Véhicule 1 (Clio V) - Deuxième location
                new Rental
                {
                    Id = 4,
                    VehicleId = 1,
                    DateStart = dateBase.AddDays(8).AddHours(10), // 18/12/2025 à 10h
                    DateEnd = dateBase.AddDays(10).AddHours(10),   // 20/12/2025 à 10h
                    UserId = 2
                }
            };
        }
    }
}