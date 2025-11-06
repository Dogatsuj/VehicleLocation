namespace VehiculeLocation.Frontend.Models
{
    public class Vehicule
    {
        public int Id { get; set; }
        public string? Modele { get; set; }
        public int Place { get; set; }
        public float DailyLocationPrice { get; set; }
        public TypeMotorisationEnum Motorisation { get; set; }
        public Boolean AutomaticTransmission { get; set; }
        public string ImagePath { get; set; }
    }

}
