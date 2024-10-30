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

        public DepotMunicipalitesSQLServer(MunicipaliteContext _context)
        {
            this.context = _context ?? throw new ArgumentNullException(nameof(context));
        }

        public MunicipaliteDTO chercherMunicipaliteParCodeGeographique(int codeGeographique)
        {
            return context
                .Municipalites
                .FirstOrDefault(m => m.mcode == codeGeographique);
        }

        public IEnumerable<MunicipaliteDTO> listerMunicipalitesActives()
        {
            return context.Municipalites.ToList();
        }

        public void DesactiverMunicipalite(MunicipaliteDTO municipalite)
        {
            municipalite.Actif = false;
            context.SaveChanges();
        }

        public void AjouterMunicipalite(MunicipaliteDTO municipalite)
        {
            context.Municipalites.Add(municipalite);
            context.SaveChanges();
        }

        public void MAJMunicipalite(MunicipaliteDTO municipalite)
        {
            MunicipaliteDTO municiapaliteExistante = context.Municipalites
                .Local
                .FirstOrDefault(m => m.mcode == municipalite.mcode);

            if (municiapaliteExistante != null)
            {
                context.Entry(municiapaliteExistante).CurrentValues.SetValues(municipalite);
            }
            else
            {
                context.Municipalites.Attach(municipalite);
                context.Entry(municipalite).State = EntityState.Modified;
            }

            context.SaveChanges();
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
