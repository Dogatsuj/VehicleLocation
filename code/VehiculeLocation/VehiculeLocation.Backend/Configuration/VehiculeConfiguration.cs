using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculeLocation.Backend.Models;

namespace VehiculeLocation.Backend.Data.Configuration
{
    /// <summary>
    /// Règle de configuration du véhicule lié a la BD
    /// </summary>
    public class VehiculeConfiguration : IEntityTypeConfiguration<Vehicule>
    {
        public void Configure(EntityTypeBuilder<Vehicule> builder)
        {
            // --------------------------------------------------------
            // Configuration des propriétés (Règles et Contraintes)
            // --------------------------------------------------------

            // Propriétés existantes
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Modele).IsRequired().HasMaxLength(50); // Exemple de contrainte
            builder.Property(v => v.Place).IsRequired();
            builder.Property(v => v.DailyLocationPrice).IsRequired(); // Assure que le prix est requis
            builder.Property(v => v.AutomaticTransmission).IsRequired(); // Assure que la transmission est requise

            // Configuration de la nouvelle ENUM Motorisation
            builder.Property(v => v.Motorisation)
                   .HasConversion<string>() // Stocke l'Enum comme un string (ex: "Essence")
                   .IsRequired();           // Motorisation est obligatoire
        }
    }
}