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
            return context.Municipalites
                .Where(m => m.Actif)
                .ToList();  
        }

        public void DesactiverMunicipalite(Municipalite municipalite)
        {
            municipalite.Actif = false;
            context.SaveChanges();
        }

        public void AjouterMunicipalite(Municipalite municipalite)
        {
            context.Municipalites.Add(municipalite);
            context.SaveChanges();
        }

        public void MAJMunicipalite(Municipalite municipalite)
        {
            context.Municipalites.Update(municipalite);
            context.SaveChanges();
        }
    }
}
