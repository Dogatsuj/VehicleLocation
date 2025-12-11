using BCrypt.Net;
using VehiculeLocation.Backend.Services;
using BCryptNet = BCrypt.Net.BCrypt; // Pour éviter les conflits de nom

public class PasswordHasherService : IPasswordHasherService
{
    // Utilise le travail par défaut (cost factor) de 12 (très sécurisé)
    public string HashPassword(string password)
    {
        // BCrypt génère le Salt unique, exécute le hachage lent,
        // et inclut le Salt dans la chaîne de hashage retournée.
        return BCryptNet.HashPassword(password, workFactor: 12);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        // BCrypt extrait le Salt du 'hashedPassword' stocké,
        // recalcule le hash, et compare les résultats.
        return BCryptNet.Verify(password, hashedPassword);
    }
}