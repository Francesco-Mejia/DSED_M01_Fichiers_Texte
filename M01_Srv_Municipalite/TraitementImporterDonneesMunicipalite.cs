using M01_Entite;

namespace M01_Srv_Municipalite
{
    public class TraitementImporterDonneesMunicipalite
    {
        private readonly IDepotImportationMunicipalite _depotImportation;
        private readonly IDepotMunicipalites _depotMunicipalites;

        public TraitementImporterDonneesMunicipalite(IDepotImportationMunicipalite depotImportation, IDepotMunicipalites depotMunicipalites)
        {
            this._depotImportation = depotImportation;
            this._depotMunicipalites = depotMunicipalites;
        }

        public StatistiquesImportationDonnees Executer()
        {
            var stats = new StatistiquesImportationDonnees();
            var municipalitesImportees = 
                _depotImportation.LireMunicipalite().ToDictionary(m => m.CodeGeographique);
            var municipalitesExistantes =
                _depotMunicipalites.listerMunicipalitesActives().ToDictionary(m => m.CodeGeographique);

            foreach (var municipalite in municipalitesImportees.Values)
            {
                if ((municipalitesExistantes.TryGetValue(municipalite.CodeGeographique, out var existante)))
                {
                    if (MunicipaliteAEteModifiee(existante, municipalite))
                    {
                        _depotMunicipalites.MAJMunicipalite(municipalite);
                        stats.NombreEnregistrementsModifies++;
                    }

                    municipalitesExistantes.Remove(municipalite.CodeGeographique);
                }
                else
                {
                    _depotMunicipalites.AjouterMunicipalite(municipalite);
                    stats.NombreEnregistrementsAjoutes++;
                }
            }

            foreach (var municipsliteADesactiver in municipalitesExistantes.Values)
            {
                _depotMunicipalites.DesactiverMunicipalite(municipsliteADesactiver);
                stats.NombreEnregistrementsDesactives++;
            }

            return stats;
        }

        private bool MunicipaliteAEteModifiee(Municipalite existante, Municipalite nouvelle)
        {
            return existante.Nom != nouvelle.Nom ||
                   existante.AdresseCourriel != nouvelle.AdresseCourriel ||
                   existante.AdresseWeb != nouvelle.AdresseWeb ||
                   existante.DateConstruction != nouvelle.DateConstruction ||
                   existante.Superficie != nouvelle.Superficie ||
                   existante.Population != nouvelle.Population;
        }
    }
}
