using M01_Entite;

namespace M01_Srv_Municipalite
{
    public class TraitementImporterDonneesMunicipalite
    {
        private readonly IDepotImportationMunicipalite depotImportation;
        private readonly IDepotMunicipalites depotMunicipalites;

        public TraitementImporterDonneesMunicipalite(IDepotImportationMunicipalite _depotImportation,
            IDepotMunicipalites _depotMunicipalites)
        {
            if (depotImportation is null)
            {
                throw new ArgumentNullException(nameof(_depotImportation));
            }
            this.depotImportation = _depotImportation;

            if (depotMunicipalites is null)
            {
                throw new ArgumentNullException(nameof(_depotMunicipalites));
            }
            this.depotMunicipalites = _depotMunicipalites;
        }

        public StatistiquesImportationDonnees Executer()
        {
            StatistiquesImportationDonnees stats = new StatistiquesImportationDonnees();

                Console.WriteLine("Début de l'importation des municipalités...");
                List<Municipalite> municipalitesImportees = depotImportation.LireMunicipalite().ToList();
                Console.WriteLine($"Nombre de municipalités importées : {municipalitesImportees.Count}");

                Console.WriteLine("Récupération des municipalités existantes...");
                List<Municipalite> municipalitesExistantes = depotMunicipalites.listerMunicipalitesActives().ToList();
                Console.WriteLine($"Nombre de municipalités existantes : {municipalitesExistantes.Count}");

                Dictionary<int, Municipalite> dictMunicipalitesExistantes = municipalitesExistantes
                    .ToDictionary(m => m.mcode);

                foreach (Municipalite municipaliteImportee in municipalitesImportees)
                {
                    if (dictMunicipalitesExistantes.TryGetValue(municipaliteImportee.mcode, out Municipalite municipaliteExistante))
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

                foreach (Municipalite municipaliteExistante in municipalitesExistantes)
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

        private bool MunicipaliteAEteModifiee(Municipalite existante, Municipalite nouvelle)
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

