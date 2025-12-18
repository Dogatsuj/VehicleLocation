using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace VehiculeLocation.Backend.Models
{
    /// <summary>
    /// Entité Véhicle correspond aux véhicules à louer
    /// </summary>
    public class Vehicle
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model{ get; set; }
        public int Seats { get; set; }
        public float DailyRentalPrice { get; set; }
        public TypeMotorisationEnum Motorisation { get; set; }
        public Boolean IsAutomaticTransmission { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
        public List<Rental> Rentals { get; set; } = new List<Rental>();

        // Propriétaire
        public int UserId { get; set; }

        // Propriété de navigation (on la met en nullable ? pour éviter les erreurs de validation au POST)
        [JsonIgnore]
        public User? User { get; set; }
    }
}
