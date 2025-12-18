namespace VehiculeLocation.Frontend.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Seats { get; set; }
        public float DailyRentalPrice { get; set; }
        public string? Motorisation { get; set; }
        public Boolean IsAutomaticTransmission { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public List<Rental> Rentals { get; set; } = new List<Rental>();
    }

}
