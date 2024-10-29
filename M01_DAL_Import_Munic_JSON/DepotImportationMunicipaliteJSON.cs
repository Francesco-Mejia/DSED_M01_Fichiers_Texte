using M01_Entite;
using Newtonsoft.Json;
using System.Net.Http;

namespace M01_DAL_Import_Munic_JSON
{
    public class DepotImportationMunicipaliteJSON : IDepotImportationMunicipalite
    {
        private readonly string url;

        public DepotImportationMunicipaliteJSON(string _url)
        {
            this.url = _url;
        }

        public IEnumerable<Municipalite> LireMunicipalite()
        {
            using (HttpClient client = new HttpClient())
            {
                string reponseJson = client.GetStringAsync(url).Result;

                dynamic data = JsonConvert.DeserializeObject(reponseJson);
                //rootObject System.Text.Json.JsonSerializer.Deserialize<>(reponseJson);

                List<Municipalite> municipalites = new List<Municipalite>();

                foreach (dynamic record in data.result.records)
                {
                    Municipalite municipalite = new Municipalite
                    {
                        mcode = int.Parse(record["mcode"].ToString()),
                        munnom = record["munnom"].ToString(),
                        mcourriel = string.IsNullOrWhiteSpace(record["mcourriel"].ToString()) ? null : record["mcourriel"].ToString(),
                        mweb = string.IsNullOrWhiteSpace(record["mweb"].ToString()) ? null : record["mweb"].ToString(),
                        mdatcons = DateTime.TryParse(record["mdatcons"].ToString(), out DateTime date) ? (DateTime?)date : null,
                        msuperf = decimal.TryParse(record["msuperf"].ToString(), out decimal superficie) ? superficie : (decimal?)null,
                        mpopul = int.TryParse(record["mpopul"].ToString(), out int population) ? population : 0,
                        Actif = true
                    };
                    municipalites.Add(municipalite);
                }
                return municipalites;
            }
        }
    }
}
