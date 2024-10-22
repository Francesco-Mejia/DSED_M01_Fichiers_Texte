using System.ComponentModel.DataAnnotations;

namespace M01_Entite
{
    public class Municipalite
    {
        [Key]
        public int mcode { get; set; }
        public string munnom { get; set; }
        public string? mcourriel { get; set; }
        public string? mweb { get; set; }
        public DateTime? mdatcons { get; set; }
        public decimal? msuperf { get; set; }
        public int? mpopul { get; set; }
        public bool Actif { get; set; } = true;
    }
}
