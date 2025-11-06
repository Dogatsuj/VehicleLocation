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

        public DbSet<Vehicule> Vehicules { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Utilisez cette ligne pour charger et appliquer toutes les configurations
            // qui implémentent IEntityTypeConfiguration dans l'assembly actuel.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // 2. Application du Seeding (Séparé)
            modelBuilder.Entity<Vehicule>().HasData(VehiculeSeeder.GetVehiculeSeedData());

        }
    }
}