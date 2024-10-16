using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_Entite
{
    public class StatistiquesImportationDonnees
    {
        public int NombreEnregistrementsAjoutes { get; set; }
        public int NombreEnregistrementsModifies { get; set; }
        public int NombreEnregistrementsDesactives { get; set; }

        public override string ToString()
        {
            return $"Ajoutés: {NombreEnregistrementsAjoutes}, Modifiés: {NombreEnregistrementsModifies}, Desactivés: {NombreEnregistrementsDesactives}";
        }
    }
}
