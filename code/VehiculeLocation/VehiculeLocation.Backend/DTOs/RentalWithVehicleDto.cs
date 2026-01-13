namespace VehiculeLocation.Backend.DTOs
{
    public class RentalWithVehicleDto
    {
        public int Id { get; set; } 
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }


        public int VehicleId { get; set; }
        public string VehicleBrand { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;

  
        public string VehicleName => $"{VehicleBrand} {VehicleModel}";

        public string? VehicleImagePath { get; set; }
        public float DailyRentalPrice { get; set; }

        public float TotalPrice { get; set; }
    }
}