using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M01_Entite
{
    public interface IDepotMunicipalites
    {
        MunicipaliteDTO chercherMunicipaliteParCodeGeographique(int codeGeographique);
        IEnumerable<MunicipaliteDTO> listerMunicipalitesActives();
        void DesactiverMunicipalite(MunicipaliteDTO municipalite);
        void AjouterMunicipalite(MunicipaliteDTO municipalite);
        void MAJMunicipalite(MunicipaliteDTO municipalite);
    }
}
