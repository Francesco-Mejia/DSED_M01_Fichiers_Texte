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

            [Fact]
            public void Executer_AvecMunicipalitesImporteesEtExistantes()
            {
                List<Municipalite> municipalitesImportees = new List<Municipalite>
                {
                    new Municipalite {mcode = 101, munnom = "Municipalite A"},
                    new Municipalite {mcode = 102, munnom = "Municipalite B"}
                };

                List<Municipalite> municipalitesExistantes = new List<Municipalite>
                {
                    new Municipalite {mcode = 101, munnom = "Municipalite A"},
                    new Municipalite {mcode = 103, munnom = "Municipalite C"}
                };

                mockDepotImportation.Setup(d => d.LireMunicipalite()).Returns(municipalitesImportees);
                mockDepotMunicipalites.Setup(d => d.listerMunicipalitesActives()).Returns(municipalitesExistantes);

                StatistiquesImportationDonnees result = service.Executer();

                mockDepotImportation.Verify(d => d.LireMunicipalite(), Times.Once);
                mockDepotMunicipalites.Verify(d => d.listerMunicipalitesActives(), Times.Once);
                Assert.NotNull(result);
            }

            [Fact]
            public void Executer_LanceException_AfficheMessageErreur()
            {
                mockDepotImportation.Setup(d => d.LireMunicipalite()).Throws(new System.Exception("Erreur de fichier"));
                var exception = Record.Exception(() => service.Executer());
                Assert.Null(exception);
            }
        }
    }
}