﻿using M01_Entite;

namespace M01_DAL_Import_Munic_CSV
{
    public class DepotImportationMunicipaliteCSV : IDepotImportationMunicipalite
    {
        private readonly string cheminFichier;

        public DepotImportationMunicipaliteCSV(string _cheminFichier)
        {
            this.cheminFichier = _cheminFichier;
        }

        public IEnumerable<MunicipaliteDTO> LireMunicipalite()
        {
            Dictionary<int, MunicipaliteDTO> municipalites = new Dictionary<int, MunicipaliteDTO>();

            using (StreamReader reader = new StreamReader(cheminFichier))
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
                        MunicipaliteDTO municipalite = new MunicipaliteDTO
                        {
                            mcode = ParserEntier(champs[0]),
                            munnom = champs[1],
                            mcourriel = string.IsNullOrWhiteSpace(champs[7]) ? null : champs[7],
                            mweb = string.IsNullOrWhiteSpace(champs[8]) ? null : champs[8],
                            mdatcons = ParseDateTime(champs[18]),
                            msuperf = ParseDecimal(champs[21]),
                            mpopul = ParserEntier(champs[22])
                        };
                        municipalites[municipalite.mcode] = municipalite;
                    }
                }
            }

            return municipalites.Values;
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
