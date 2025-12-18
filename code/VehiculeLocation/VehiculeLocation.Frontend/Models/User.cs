namespace VehiculeLocation.Frontend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        // On ne met JAMAIS le mot de passe dans le modèle Front
    }
}