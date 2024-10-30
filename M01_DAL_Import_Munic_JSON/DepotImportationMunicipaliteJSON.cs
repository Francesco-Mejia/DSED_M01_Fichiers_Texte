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

        public IEnumerable<MunicipaliteDTO> LireMunicipalite()
        {
            using (HttpClient client = new HttpClient())
            {
                string reponseJson = client.GetStringAsync(url).Result;

                RootObject rootObject = System.Text.Json.JsonSerializer.Deserialize<RootObject>(reponseJson);

                List<MunicipaliteDTO> municipalites = new List<MunicipaliteDTO>();

                foreach (Record record in rootObject.result.records)
                {
                    MunicipaliteDTO municipalite = new MunicipaliteDTO
                    {
                        mcode = int.Parse(record.mcode),
                        munnom = record.munnom,
                        mcourriel = string.IsNullOrWhiteSpace(record.mcourriel) ? null : record.mcourriel,
                        mweb = string.IsNullOrWhiteSpace(record.mweb) ? null : record.mweb,
                        mdatcons = DateTime.TryParse(record.mdatcons, out DateTime date) ? (DateTime?)date : null,
                        msuperf = record.msuperf,
                        mpopul = record.mpopul,
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
        public decimal msuperf { get; set; }
        public int mpopul { get; set; }
    }
}
