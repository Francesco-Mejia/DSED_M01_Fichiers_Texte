using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M01_DAL_Import_Munic_CSV;
using M01_DAL_Municipalite_SQLServer;
using M01_Entite;
using M01_Srv_Municipalite;

namespace DSED_M01_Fichiers_Texte
{
    public class TraitementService
    {
        private readonly TraitementImporterDonneesMunicipalite traitement;

        public TraitementService(TraitementImporterDonneesMunicipalite _traitement)
        {
            this.traitement = _traitement;
        }

        public void Executer()
        {
            Console.WriteLine("Début du traitement!");

            StatistiquesImportationDonnees statistiques = traitement.Executer();

            Console.WriteLine("Traitement exécuté avec succès");
            Console.WriteLine("Statistiques d'importation:");
            Console.WriteLine(statistiques.ToString());
        }
    }
}
