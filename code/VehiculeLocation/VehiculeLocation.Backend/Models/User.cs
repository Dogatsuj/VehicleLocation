using System.Text.Json.Serialization;

namespace VehiculeLocation.Backend.Models
{
    /// <summary>
    /// Utilisateur du site
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        // Les véhicules possédés par cet utilisateur
        [JsonIgnore]
        public List<Vehicle> OwnedVehicles { get; set; } = new List<Vehicle>();

        // Les locations effectuées par cet utilisateur
        [JsonIgnore]
        public List<Rental> MyRentals { get; set; } = new List<Rental>();
    }
}