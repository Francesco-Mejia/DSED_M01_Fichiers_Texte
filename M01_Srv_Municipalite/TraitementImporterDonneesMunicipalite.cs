﻿using M01_Entite;

namespace M01_Srv_Municipalite
{
    public class TraitementImporterDonneesMunicipalite
    {
        private readonly IDepotImportationMunicipalite _depotImportation;
        private readonly IDepotMunicipalites _depotMunicipalites;

        public TraitementImporterDonneesMunicipalite(IDepotImportationMunicipalite depotImportation,
            IDepotMunicipalites depotMunicipalites)
        {
            this._depotImportation = depotImportation ?? throw new ArgumentNullException(nameof(depotImportation));
            this._depotMunicipalites =
                depotMunicipalites ?? throw new ArgumentNullException(nameof(depotMunicipalites));
        }

        public StatistiquesImportationDonnees Executer()
        {
            var stats = new StatistiquesImportationDonnees();
            try
            {
                Console.WriteLine("Début de l'importation des municipalités...");
                var municipalitesImportees = _depotImportation.LireMunicipalite().ToList();
                Console.WriteLine($"Nombre de municipalités importées : {municipalitesImportees.Count}");

                Console.WriteLine("Récupération des municipalités existantes...");
                var municipalitesExistantes = _depotMunicipalites.listerMunicipalitesActives().ToList();
                Console.WriteLine($"Nombre de municipalités existantes : {municipalitesExistantes.Count}");

                // Le reste du code...
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite lors de l'exécution : {ex.Message}");
                Console.WriteLine($"Stack Trace : {ex.StackTrace}");
            }

            return stats;
        }




        private bool MunicipaliteAEteModifiee(Municipalite existante, Municipalite nouvelle)
        {
            return existante.Nom != nouvelle.Nom ||
                   existante.AdresseCourriel != nouvelle.AdresseCourriel ||
                   existante.AdresseWeb != nouvelle.AdresseWeb ||
                   existante.DateConstruction != nouvelle.DateConstruction ||
                   existante.Superficie != nouvelle.Superficie ||
                   existante.Population != nouvelle.Population;
        }
    }
}

