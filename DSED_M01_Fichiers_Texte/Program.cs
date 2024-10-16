using M01_DAL_Import_Munic_CSV;
using M01_DAL_Municipalite_SQLServer;
using M01_Srv_Municipalite;

namespace DSED_M01_Fichiers_Texte
{
    public class Program
    {
        static void Main(string[] args)
        {
            var depotImportation = new DepotImportationMunicipaliteCSV("\"C:\\CSFOY AUTOMNE 2024\\Developpement_de_services_echange_donnees\\Exercices\\MUN.csv\"");

            var depotMunicipalites = new DepotMunicipalitesSQLServer();

            var traitement = new TraitementImporterDonneesMunicipalite(depotImportation, depotMunicipalites);

            var statistiques = traitement.Executer();

            Console.WriteLine("Statistiques d'importation:");
            Console.WriteLine(statistiques.ToString());
        }
    }
}
