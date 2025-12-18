using System.Text.Json.Serialization;

namespace VehiculeLocation.Backend.Models
{
    /// <summary>
    /// Location d'un véhicule a une data spécifique
    /// </summary>
    public class Rental
    {
        public int Id { get; set; }
        
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        // Clé étrangère pour lier à Vehicule
        public int VehicleId { get; set; }
        // Personne qui loue la voiture
        public int UserId { get; set; }

        // Propriété de navigation vers le véhicule (Utilisation de [JsonIgnore] si nécessaire)
        [JsonIgnore]
        public Vehicle? Vehicle { get; set; }
        // Propriété de navigation vers le loueur (Utilisation de [JsonIgnore] si nécessaire)
        [JsonIgnore]
        public User? User { get; set; }
    }
}