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

                RootObject rootObject = System.Text.Json.JsonSerializer.Deserialize<RootObject>(reponseJson);

                List<Municipalite> municipalites = new List<Municipalite>();

                foreach (Record record in rootObject.result.records)
                {
                    Municipalite municipalite = new Municipalite
                    {
                        mcode = int.Parse(record.mcode),
                        munnom = record.munnom,
                        mcourriel = string.IsNullOrWhiteSpace(record.mcourriel) ? null : record.mcourriel,
                        mweb = string.IsNullOrWhiteSpace(record.mweb) ? null : record.mweb,
                        mdatcons = DateTime.TryParse(record.mdatcons, out DateTime date) ? (DateTime?)date : null,
                        msuperf = decimal.TryParse(record.msuperf, out decimal superficie) ? superficie : (decimal?)null,
                        mpopul = int.TryParse(record.mpopul, out int population) ? population : 0,
                        Actif = true
                    };
                    municipalites.Add(municipalite);
                }
                return municipalites;
            }
        }
    }

    public class RootObject
    {
        public Result result { get; set; }
    }

    public class Result
    {
        public Record[] records { get; set;}
    }

    public class Record
    {
        public string mcode { get; set; }
        public string munnom { get; set; }
        public string mcourriel { get; set; }
        public string mweb { get; set; }
        public string mdatcons { get; set; }
        public string msuperf { get; set; }
        public string mpopul { get; set; }
    }
}
