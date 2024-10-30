using M01_Entite;

namespace M01_Srv_Municipalite
{
    public class TraitementImporterDonneesMunicipalite
    {
        private readonly IDepotImportationMunicipalite depotImportation;
        private readonly IDepotMunicipalites depotMunicipalites;

        public TraitementImporterDonneesMunicipalite(IDepotImportationMunicipalite depotImportation,
            IDepotMunicipalites depotMunicipalites)
        {
            this.depotImportation = depotImportation ?? throw new ArgumentNullException(nameof(depotImportation));
            this.depotMunicipalites =
                depotMunicipalites ?? throw new ArgumentNullException(nameof(depotMunicipalites));
        }

        public StatistiquesImportationDonnees Executer()
        {
            StatistiquesImportationDonnees stats = new StatistiquesImportationDonnees();

            Console.WriteLine("Début de l'importation des municipalités...");
            List<MunicipaliteDTO> municipalitesImportees = depotImportation.LireMunicipalite().ToList();
            Console.WriteLine($"Nombre de municipalités importées : {municipalitesImportees.Count}");

            Console.WriteLine("Récupération des municipalités existantes...");
            List<MunicipaliteDTO> municipalitesExistantes = depotMunicipalites.listerMunicipalitesActives().ToList();
            Console.WriteLine($"Nombre de municipalités existantes : {municipalitesExistantes.Count}");

            Dictionary<int, MunicipaliteDTO> dictMunicipalitesExistantes = municipalitesExistantes
                .ToDictionary(m => m.mcode);

            foreach (MunicipaliteDTO municipaliteImportee in municipalitesImportees)
            {
                if (dictMunicipalitesExistantes.TryGetValue(municipaliteImportee.mcode, out MunicipaliteDTO municipaliteExistante))
                {
                    if (MunicipaliteAEteModifiee(municipaliteExistante, municipaliteImportee))
                    {
                        depotMunicipalites.MAJMunicipalite(municipaliteImportee);
                        stats.NombreEnregistrementsModifies++;
                    }
                }
                else
                {
                    depotMunicipalites.AjouterMunicipalite(municipaliteImportee);
                    stats.NombreEnregistrementsAjoutes++;
                }
            }

            foreach (MunicipaliteDTO municipaliteExistante in municipalitesExistantes)
            {
                if (!municipalitesImportees.Any(m => m.mcode == municipaliteExistante.mcode))
                {
                    depotMunicipalites.DesactiverMunicipalite(municipaliteExistante);
                    stats.NombreEnregistrementsDesactives++;
                }
            }

            Console.WriteLine("Traitement exécuté avec succès.");
            Console.WriteLine($"Statistiques d'importation: Ajoutés: {stats.NombreEnregistrementsAjoutes}, Modifiés: {stats.NombreEnregistrementsModifies}" +
                              $", Désactivés: {stats.NombreEnregistrementsDesactives}");

            return stats;
        }

        private bool MunicipaliteAEteModifiee(MunicipaliteDTO existante, MunicipaliteDTO nouvelle)
        {
            return existante.munnom != nouvelle.munnom ||
                   existante.mcourriel != nouvelle.mcourriel ||
                   existante.mweb != nouvelle.mweb ||
                   existante.mdatcons != nouvelle.mdatcons ||
                   existante.msuperf != nouvelle.msuperf ||
                   existante.mpopul != nouvelle.mpopul;
        }
    }
}

