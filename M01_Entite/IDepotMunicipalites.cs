using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_Entite
{
    public interface IDepotMunicipalites
    {
        Municipalite chercherMunicipaliteParCodeGeographique(int codeGeographique);
        IEnumerable<Municipalite> listerMunicipalitesActives();
        void DesactiverMunicipalite(Municipalite municipalite);
        void AjouterMunicipalite(Municipalite municipalite);
        void MAJMunicipalite(Municipalite municipalite);
    }
}
