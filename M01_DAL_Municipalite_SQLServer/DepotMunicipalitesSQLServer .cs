using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M01_Entite;

namespace M01_DAL_Municipalite_SQLServer
{
    public class DepotMunicipalitesSQLServer : IDepotMunicipalites
    {
        private readonly MunicipaliteContext context;

        public DepotMunicipalitesSQLServer()
        {
            this.context = new MunicipaliteContext();
        }

        public Municipalite chercherMunicipaliteParCodeGeographique(int codeGeographique)
        {
            return context.Municipalites
                .FirstOrDefault(m => m.CodeGeographique == codeGeographique);
        }

        public IEnumerable<Municipalite> listerMunicipalitesActives()
        {
            throw new NotImplementedException();
        }

        public void DesactiverMunicipalite(Municipalite municipalite)
        {
            throw new NotImplementedException();
        }

        public void AjouterMunicipalite(Municipalite municipalite)
        {
            throw new NotImplementedException();
        }

        public void MAJMunicipalite(Municipalite municipalite)
        {
            throw new NotImplementedException();
        }
    }
}
