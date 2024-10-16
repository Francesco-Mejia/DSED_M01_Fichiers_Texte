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
            var municipalites = new List<Municipalite>();

            using (var reader = new StreamReader(_cheminFichier))
            {
                reader.ReadLine();

                string ligne;
                while ((ligne = reader.ReadLine()) != null)
                {
                    string[] champs = ligne.Split(',');
                    if (champs.Length >= 7)
                    {
                        municipalites.Add(new Municipalite
                        {
                            CodeGeographique = int.Parse(champs[0]),
                            Nom = champs[1],
                            AdresseCourriel = champs[7],
                            AdresseWeb = champs[8],
                            DateConstruction = champs[18],
                            Superficie = double.Parse(champs[21]),
                            Population = int.Parse(champs[22])
                        });
                    }
                }
            }

            return municipalites;
        }
    }
}
