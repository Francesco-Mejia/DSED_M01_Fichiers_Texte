using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_Entite
{
    public class Municipalite
    {
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
