using M01_DAL_Import_Munic_CSV;
using M01_DAL_Municipalite_SQLServer;
using M01_Entite;
using M01_Srv_Municipalite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DSED_M01_Fichiers_Texte
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                TraitementService traitementService = services.GetRequiredService<TraitementService>();
                traitementService.Executer();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
                .CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    IConfiguration configuration = context.Configuration;

                    services.AddDbContext<MunicipaliteContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                    services.AddTransient<IDepotImportationMunicipalite, DepotImportationMunicipaliteCSV>(provider =>
                        new DepotImportationMunicipaliteCSV("MUN.csv"));
                    services.AddTransient<IDepotMunicipalites, DepotMunicipalitesSQLServer>();

                    services.AddTransient<TraitementImporterDonneesMunicipalite>();
                    services.AddTransient<TraitementService>();
                });
    }
}
