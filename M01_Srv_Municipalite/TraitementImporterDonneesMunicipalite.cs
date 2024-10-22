using M01_Entite;

namespace M01_Srv_Municipalite
{
    public class TraitementImporterDonneesMunicipalite
    {
        private readonly IDepotImportationMunicipalite _depotImportation;
        private readonly IDepotMunicipalites _depotMunicipalites;

        public TraitementImporterDonneesMunicipalite(IDepotImportationMunicipalite depotImportation,
            IDepotMunicipalites depotMunicipalites)
        {
            this._depotImportation = depotImportation ?? throw new ArgumentNullException(nameof(depotImportation));
            this._depotMunicipalites =
                depotMunicipalites ?? throw new ArgumentNullException(nameof(depotMunicipalites));
        }

        public StatistiquesImportationDonnees Executer()
        {
            var stats = new StatistiquesImportationDonnees();
            try
            {
                Console.WriteLine("Début de l'importation des municipalités...");
                var municipalitesImportees = _depotImportation.LireMunicipalite().ToList();
                Console.WriteLine($"Nombre de municipalités importées : {municipalitesImportees.Count}");

                Console.WriteLine("Récupération des municipalités existantes...");
                var municipalitesExistantes = _depotMunicipalites.listerMunicipalitesActives().ToList();
                Console.WriteLine($"Nombre de municipalités existantes : {municipalitesExistantes.Count}");

                var dictMunicipalitesExistantes = municipalitesExistantes
                    .ToDictionary(m => m.mcode);

                foreach (var municipaliteImportee in municipalitesImportees)
                {
                    if (dictMunicipalitesExistantes.TryGetValue(municipaliteImportee.mcode, out var municipaliteExistante))
                    {
                        if (MunicipaliteAEteModifiee(municipaliteExistante, municipaliteImportee))
                        {
                            _depotMunicipalites.MAJMunicipalite(municipaliteImportee);
                            stats.NombreEnregistrementsModifies++;
                        }
                    }
                    else
                    {
                        _depotMunicipalites.AjouterMunicipalite(municipaliteImportee);
                        stats.NombreEnregistrementsAjoutes++;
                    }
                }

                foreach (var municipaliteExistante in municipalitesExistantes)
                {
                    if (!municipalitesImportees.Any(m => m.mcode == municipaliteExistante.mcode))
                    {
                        _depotMunicipalites.DesactiverMunicipalite(municipaliteExistante);
                        stats.NombreEnregistrementsDesactives++;
                    }
                }

                Console.WriteLine("Traitement exécuté avec succès.");
                Console.WriteLine($"Statistiques d'importation: Ajoutés: {stats.NombreEnregistrementsAjoutes}, Modifiés: {stats.NombreEnregistrementsModifies}, Désactivés: {stats.NombreEnregistrementsDesactives}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite lors de l'exécution : {ex.Message}");
                Console.WriteLine($"Stack Trace : {ex.StackTrace}");
            }

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

