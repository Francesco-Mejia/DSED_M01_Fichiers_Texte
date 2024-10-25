using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using M01_DAL_Import_Munic_CSV;
using M01_DAL_Municipalite_SQLServer;
using M01_Entite;
using M01_Srv_Municipalite;

namespace DSED_M01_Fichiers_Texte
{
    public class InitialisateurHote
    {
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
