using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehiculeLocation.Backend.Models;

namespace VehiculeLocation.Backend.Configuration
{
    /// <summary>
    /// Règle de configuration de l'utilisateur lié a la BD
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // --------------------------------------------------------
            // Configuration des propriétés (Règles et Contraintes)
            // --------------------------------------------------------

            // Propriétés existantes
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Password).IsRequired();
            builder.Property(v => v.Username).IsRequired();
            builder.Property(v => v.IsAdmin).HasDefaultValue(false);

            // Un User peut avoir plusieurs Véhicules (Propriétaire)
            builder.HasMany(u => u.OwnedVehicles)
                   .WithOne(v => v.User)
                   .HasForeignKey(v => v.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // On ne supprime pas l'utilisateur si ses voitures existent

            // Un User peut avoir plusieurs Locations (Loueur)
            builder.HasMany(u => u.MyRentals)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
