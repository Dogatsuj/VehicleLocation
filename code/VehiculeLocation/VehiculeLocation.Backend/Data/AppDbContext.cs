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
            // seeding Vehicle
            modelBuilder.Entity<Vehicle>().HasData(VehicleSeeder.GetVehiculeSeedData());
            // seeding location
            modelBuilder.Entity<Rental>().HasData(RentalSeeder.GetLocationSeedData());
            // seeding User
            modelBuilder.Entity<User>().HasData(UserSeeder.GetUserSeedData());

            // Relation des tables
            // Relation one to many de location
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Rentals) // Un Vehicule a plusieurs Locations
                .WithOne(l => l.Vehicle)  // Chaque Location appartient à un Vehicule
                .HasForeignKey(l => l.VehicleId) // Utilise VehiculeId comme clé étrangère
                .OnDelete(DeleteBehavior.Cascade); // Les locations sont supprimées si le véhicule l'est
        }
    }
}