using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VehiculeLocation.Backend.Data.Seeding;
using VehiculeLocation.Backend.Models;

namespace VehiculeLocation.Backend.Data
{
    /// <summary>
    /// Gère la création et l'insertion des données
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Rental> Locations { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Charge les configurateurs
            // Vehicule configurateur
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Application du Seeding
            // seeding User
            modelBuilder.Entity<User>().HasData(UserSeeder.GetUserSeedData());
            // seeding Vehicle
            modelBuilder.Entity<Vehicle>().HasData(VehicleSeeder.GetVehiculeSeedData());
            // seeding location
            modelBuilder.Entity<Rental>().HasData(RentalSeeder.GetLocationSeedData());

            // Relation des tables
            // Relation one to many de location
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Rentals) // Un Vehicule a plusieurs Locations
                .WithOne(l => l.Vehicle)  // Chaque Location appartient à un Vehicule
                .HasForeignKey(l => l.VehicleId) // Utilise VehiculeId comme clé étrangère
                .OnDelete(DeleteBehavior.Restrict); // Les locations sont supprimées si le véhicule l'est

            // Relation Loueur : Un utilisateur effectue plusieurs locations
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.User)
                .WithMany(u => u.MyRentals)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Si le user disparait, ses locations aussi
        }
    }
}