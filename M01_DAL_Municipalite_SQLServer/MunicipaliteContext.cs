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
                try
                {
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

                    string connectionString = configuration.GetConnectionString("DefaultConnection");
                    optionsBuilder.UseSqlServer(connectionString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la configuration du contexte : {ex.Message}");
                    throw;
                }
            } 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipalite>(entity =>
            {
                entity.ToTable("municipalites");

                entity.HasKey(m => m.CodeGeographique);

                entity.Property(m => m.CodeGeographique)
                    .HasColumnName("mcode")
                    .ValueGeneratedNever();

                entity.Property(m => m.Nom)
                    .HasColumnName("munnom")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(m => m.AdresseCourriel)
                    .HasColumnName("mcourriel")
                    .HasMaxLength(255)
                    .IsRequired(false);

                entity.Property(m => m.AdresseWeb)
                    .HasColumnName("mweb")
                    .HasMaxLength(255)
                    .IsRequired(false);

                entity.Property(m => m.DateConstruction)
                    .HasColumnName("mdatcons")
                    .HasColumnType("date")
                    .IsRequired(false);

                entity.Property(m => m.Superficie)
                    .HasColumnName("msuperf")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired(false);

                entity.Property(m => m.Population)
                    .HasColumnName("mpopul")
                    .IsRequired(false);

                entity.Property(m => m.Actif)
                    .HasDefaultValue(true);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
