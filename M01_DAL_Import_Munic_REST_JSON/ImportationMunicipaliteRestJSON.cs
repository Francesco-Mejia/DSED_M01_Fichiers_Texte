using M01_Entite;
using System;
using System.ComponentModel.Design;
using System.Net.Http.Headers;

namespace M01_DAL_Import_Munic_REST_JSON
{
    public class ImportationMunicipaliteRestJSON : IDepotImportationMunicipalite
    {
        private readonly HttpClient client;
        private const string BaseURL = "https://www.donneesquebec.ca";
        private const string RessourceURL = "https://www.donneesquebec.ca/recherche/api/action/datastore_search?resource_id=19385b4e-5503-4330-9e59-f998f5918363&limit=3000";
        public ImportationMunicipaliteRestJSON()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri(BaseURL);
            this.client.DefaultRequestHeaders.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public IEnumerable<MunicipaliteDTO> LireMunicipalite()
        {
            HttpResponseMessage reponse = this.client.GetAsync(RessourceURL).Result;

            if (reponse.IsSuccessStatusCode)
            {
                string contenuJSON = reponse.Content.ReadAsStringAsync().Result;

                RootObject rootObject = System.Text.Json.JsonSerializer.Deserialize<RootObject>(contenuJSON);

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
            else
            {
                throw new HttpRequestException("La requête n'as pas reussi");
            }
        }
    }

    public class RootObject
    {
        public Result result { get; set; }
    }

    public class Result
    {
        public Record[] records { get; set; }
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
