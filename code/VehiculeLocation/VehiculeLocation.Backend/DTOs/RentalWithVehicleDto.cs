namespace VehiculeLocation.Backend.DTOs
{
    public class RentalWithVehicleDto
    {
        // Infos de la location
        public int Id { get; set; } // ID de la location
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        // Infos du véhicule (aplaties)
        public int VehicleId { get; set; }
        public string VehicleBrand { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;

        // C'est souvent pratique de combiner Marque + Modèle pour l'affichage direct
        public string VehicleName => $"{VehicleBrand} {VehicleModel}";

        public string? VehicleImagePath { get; set; }
        public float DailyRentalPrice { get; set; }

        // Calcul du prix total (optionnel mais pratique pour le front)
        public float TotalPrice { get; set; }
    }
}