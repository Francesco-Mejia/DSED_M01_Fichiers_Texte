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
using M01_DAL_Import_Munic_JSON;

namespace DSED_M01_Fichiers_Texte
{
    public class InitialisateurHote
    {
        public static IHostBuilder CreateHostBuilderCSV(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                IConfiguration configuration = context.Configuration;

                services.AddDbContext<MunicipaliteContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                services.AddSingleton<IDepotImportationMunicipalite, DepotImportationMunicipaliteCSV>(provider =>
                    new DepotImportationMunicipaliteCSV("MUN.csv"));
                services.AddSingleton<IDepotMunicipalites, DepotMunicipalitesSQLServer>();

                services.AddSingleton<TraitementImporterDonneesMunicipalite>();
                services.AddSingleton<TraitementService>();
            });

        public static IHostBuilder CreateHostBuilderJSON(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                IConfiguration configuration = context.Configuration;

                services.AddDbContext<MunicipaliteContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                services.AddSingleton<IDepotImportationMunicipalite, DepotImportationMunicipaliteJSON>(provider =>
                    new DepotImportationMunicipaliteJSON("https://www.donneesquebec.ca/recherche/api/action/datastore_search?resource_id=19385b4e-5503-4330-9e59-f998f5918363&limit=3000"));
                services.AddSingleton<IDepotMunicipalites, DepotMunicipalitesSQLServer>();

                services.AddSingleton<TraitementImporterDonneesMunicipalite>();
                services.AddSingleton<TraitementService>();
            });
    }
}
