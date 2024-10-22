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
                    string[] champs = ligne.Split("\",\"")
                        .Select(c => c.Trim('"'))
                        .ToArray();

                    if (champs.Length >= 23)
                    {
                        municipalites.Add(new Municipalite
                        {
                            CodeGeographique = ParserEntier(champs[0]),
                            Nom = champs[1],
                            AdresseCourriel = string.IsNullOrWhiteSpace(champs[7]) ? null : champs[7],
                            AdresseWeb = string.IsNullOrWhiteSpace(champs[8]) ? null : champs[8],
                            DateConstruction = ParseDateTime(champs[18]),
                            Superficie = ParseDecimal(champs[21]),
                            Population = ParserEntier(champs[22])
                        });
                    }
                }
            }

            return municipalites;
        }

        private int ParserEntier(string valeur)
        {
            return int.TryParse(valeur, out int result) ? result : 0;
        }

        private decimal? ParseDecimal(string valeur)
        {
            return decimal.TryParse(valeur, out decimal result) ? (decimal)result : null;
        }

        private DateTime? ParseDateTime(string valeur)
        {
            return DateTime.TryParse(valeur, out DateTime result) ? (DateTime)result : null;
        }
    }
}
