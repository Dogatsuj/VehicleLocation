using VehiculeLocation.Backend.Models;
using System.Collections.Generic;
using System;

namespace VehiculeLocation.Backend.Data.Seeding
{
    public static class UserSeeder
    {
        /// <summary>
        /// Ajout des entités dans la table Location
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<User> GetUserSeedData()
        {
            // Définir une date de référence stable pour le seeding
            var dateBase = new DateTime(2025, 12, 10);

            return new List<User>
            {
                // Location 1 : Véhicule 1 (Clio V) - 2 jours
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "$2a$12$.SP2dh9GQr/nVpFqTd.XGudDM/FkyBE8gKoXQmmTmE08tejW6SOb6",
                    IsAdmin = true
                },
                
                // User 2 : Regular User
                new User
                {
                    Id = 2,
                    Username = "Grigory Ivanovich Kulik",
                    Password = "$2a$12$ztnvLsPVjDZ7dDq2y.K4seU6x47uk/CinTHGN.OaY3rwrSGpVqEDC"
                }
            };
        }
    }
}