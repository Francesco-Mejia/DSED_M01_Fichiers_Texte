using Xunit;
using Moq;
using M01_DAL_Municipalite_SQLServer;
using M01_Srv_Municipalite;
using M01_Entite;
using System.Collections.Generic;

namespace M01_Tests
{
    public class M01_Tests
    {
        public class TraitementImporterDonneesMunicipalitesTests
        {
            private readonly Mock<IDepotImportationMunicipalite> mockDepotImportation;
            private readonly Mock<IDepotMunicipalites> mockDepotMunicipalites;
            private readonly TraitementImporterDonneesMunicipalite service;

            public TraitementImporterDonneesMunicipalitesTests()
            {
                this.mockDepotImportation = new Mock<IDepotImportationMunicipalite>();
                this.mockDepotMunicipalites = new Mock<IDepotMunicipalites>();
                this.service =
                    new TraitementImporterDonneesMunicipalite(mockDepotImportation.Object,
                        mockDepotMunicipalites.Object);
            }
        } 
    }
}