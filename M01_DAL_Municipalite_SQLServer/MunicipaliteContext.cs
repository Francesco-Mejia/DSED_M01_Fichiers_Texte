using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using M01_Entite;
using System.IO;

namespace M01_DAL_Municipalite_SQLServer
{
    public class MunicipaliteContext : DbContext
    {
        public DbSet<Municipalite> Municipalites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            } 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipalite>()
                .HasKey(m => m.CodeGeographique);

            modelBuilder.Entity<Municipalite>()
                .Property(m => m.Nom)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Municipalite>()
                .Property(m => m.AdresseCourriel)
                .HasMaxLength(255);

            modelBuilder.Entity<Municipalite>()
                .Property(m => m.AdresseWeb)
                .HasMaxLength(255);

            modelBuilder.Entity<Municipalite>()
                .Property(m => m.DateConstruction)
                .HasColumnType("date");

            base.OnModelCreating(modelBuilder);
        }
    }
}
