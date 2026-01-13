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


        public int VehicleId { get; set; }
        public string VehicleBrand { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;


        public string VehicleName { get; set; } = string.Empty;

        public string? VehicleImagePath { get; set; }
        public float DailyRentalPrice { get; set; }

        public float TotalPrice { get; set; }
    }
}