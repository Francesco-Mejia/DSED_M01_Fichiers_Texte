using M01_DAL_Import_Munic_CSV;
using M01_DAL_Municipalite_SQLServer;
using M01_Entite;
using M01_Srv_Municipalite;

namespace DSED_M01_Fichiers_Texte
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Début du programme.");

                DepotImportationMunicipaliteCSV depotImportation = new DepotImportationMunicipaliteCSV("MUN.csv");
                Console.WriteLine("Dépôt d'importation créé avec succès");

                DepotMunicipalitesSQLServer depotMunicipalites = new DepotMunicipalitesSQLServer();
                Console.WriteLine("Dépôt de municipalités créé avec succès");

                TraitementImporterDonneesMunicipalite traitement = new TraitementImporterDonneesMunicipalite(depotImportation, depotMunicipalites);
                Console.WriteLine("Traitement créé avec succès.");

                Console.WriteLine("Début de l'exécution du traitement...");
                StatistiquesImportationDonnees statistiques = traitement.Executer();
                Console.WriteLine("Traitement exécuté avec succès");

                Console.WriteLine("Statistiques d'importation:");
                Console.WriteLine(statistiques.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
                Console.WriteLine($"Stack Trace : {ex.StackTrace}");
            }
        }
    }
}
