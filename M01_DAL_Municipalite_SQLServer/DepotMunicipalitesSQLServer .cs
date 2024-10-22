using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M01_Entite;
using Microsoft.EntityFrameworkCore;

namespace M01_DAL_Municipalite_SQLServer
{
    public class DepotMunicipalitesSQLServer : IDepotMunicipalites
    {
        private readonly MunicipaliteContext context;

        public DepotMunicipalitesSQLServer()
        {
            context = new MunicipaliteContext();
            if (context == null)
            {
                throw new InvalidOperationException("Le contexte n'a pas pu être initialisé.");
            }
            Console.WriteLine("Contexte initialisé avec succès.");
        }

        public Municipalite chercherMunicipaliteParCodeGeographique(int codeGeographique)
        {
            return context.Municipalites
                .FirstOrDefault(m => m.CodeGeographique == codeGeographique);
        }

        public IEnumerable<Municipalite> listerMunicipalitesActives()
        {
            return context.Municipalites.ToList();
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

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
