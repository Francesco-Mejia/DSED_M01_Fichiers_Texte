using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_Entite
{
    public interface IDepotImportationMunicipalite
    {
        IEnumerable<MunicipaliteDTO> LireMunicipalite();
    }
}
