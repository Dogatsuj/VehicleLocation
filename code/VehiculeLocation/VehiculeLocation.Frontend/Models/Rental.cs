using System.Text.Json.Serialization;

namespace VehiculeLocation.Frontend.Models
{
    /// <summary>
    /// Location d'un véhicule a une data spécifique
    /// </summary>

    public class Rental
    {
        public int Id { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public int VehicleId { get; set; } // Pour savoir quelle voiture on loue
        public int UserId { get; set; }    // Pour savoir qui loue (sera rempli par le back via token)
    }
}