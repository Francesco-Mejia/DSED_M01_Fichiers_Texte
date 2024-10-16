using M01_Entite;

namespace M01_DAL_Import_Munic_CSV
{
    public class DepotImportationMunicipaliteCSV : IDepotImportationMunicipalite
    {
        private readonly string _cheminFichier;

        public DepotImportationMunicipaliteCSV(string cheminFichier)
        {
            this._cheminFichier = cheminFichier;
        }

        public IEnumerable<Municipalite> LireMunicipalite()
        {
            
        }
    }
}
