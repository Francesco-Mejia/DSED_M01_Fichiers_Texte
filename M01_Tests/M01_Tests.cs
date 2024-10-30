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
                List<MunicipaliteDTO> municipalitesImportees = new List<MunicipaliteDTO>
                {
                    new MunicipaliteDTO {mcode = 101, munnom = "Municipalite A"},
                    new MunicipaliteDTO {mcode = 102, munnom = "Municipalite B"}
                };

                List<MunicipaliteDTO> municipalitesExistantes = new List<MunicipaliteDTO>
                {
                    new MunicipaliteDTO {mcode = 101, munnom = "Municipalite A"},
                    new MunicipaliteDTO {mcode = 103, munnom = "Municipalite C"}
                };

                mockDepotImportation.Setup(d => d.LireMunicipalite()).Returns(municipalitesImportees);
                mockDepotMunicipalites.Setup(d => d.listerMunicipalitesActives()).Returns(municipalitesExistantes);

                StatistiquesImportationDonnees resultat = service.Executer();

                mockDepotImportation.Verify(d => d.LireMunicipalite(), Times.Once);
                mockDepotMunicipalites.Verify(d => d.listerMunicipalitesActives(), Times.Once);
                Assert.NotNull(resultat);
            }

            [Fact]
            public void Executer_AvecAucuneMunicipaliteImportee()
            {
                List<MunicipaliteDTO> municipalitesImportees = new List<MunicipaliteDTO>();

                List<MunicipaliteDTO> municipalitesExistantes = new List<MunicipaliteDTO>
                {
                    new MunicipaliteDTO {mcode = 101, munnom = "Municipalite A"},
                    new MunicipaliteDTO {mcode = 102, munnom = "Municipalite B"}
                };

                mockDepotImportation.Setup(d => d.LireMunicipalite()).Returns(municipalitesImportees);
                mockDepotMunicipalites.Setup(d => d.listerMunicipalitesActives()).Returns(municipalitesExistantes);

                StatistiquesImportationDonnees resultat = service.Executer();

                Assert.Equal(2, resultat.NombreEnregistrementsDesactives);
                Assert.Equal(0, resultat.NombreEnregistrementsAjoutes);
                Assert.Equal(0, resultat.NombreEnregistrementsModifies);
            }

            [Fact]
            public void Executer_AvecMunicipalitesReimportees()
            {
                List<MunicipaliteDTO> municipalitesImportees = new List<MunicipaliteDTO>
                {
                    new MunicipaliteDTO {mcode = 101, munnom = "Municipalite A MAJ"},
                    new MunicipaliteDTO {mcode = 102, munnom = "Municipalite B"},
                    new MunicipaliteDTO {mcode = 103, munnom = "Municipalite C"}
                };

                List<MunicipaliteDTO> municipalitesExistantes = new List<MunicipaliteDTO>
                {
                    new MunicipaliteDTO {mcode = 101, munnom = "Municipalite A"},
                    new MunicipaliteDTO {mcode = 102, munnom = "Municipalite B"},
                    new MunicipaliteDTO {mcode = 104, munnom = "Municipalite D"}
                };

                mockDepotImportation.Setup(d => d.LireMunicipalite()).Returns(municipalitesImportees);
                mockDepotMunicipalites.Setup(d => d.listerMunicipalitesActives()).Returns(municipalitesExistantes);

                StatistiquesImportationDonnees resultat = service.Executer();

                Assert.Equal(1, resultat.NombreEnregistrementsDesactives);
                Assert.Equal(1, resultat.NombreEnregistrementsAjoutes);
                Assert.Equal(1, resultat.NombreEnregistrementsModifies);
                mockDepotMunicipalites.Verify(d => d.MAJMunicipalite(It.Is<MunicipaliteDTO>(m => m.mcode == 101 && m.munnom == "Municipalite A MAJ")), Times.Once);
            }

            [Fact]
            public void Executer_AvecToutesMunicipalitesNouvelles()
            {
                List<MunicipaliteDTO> municipalitesImportees = new List<MunicipaliteDTO>
                {
                    new MunicipaliteDTO {mcode = 201, munnom = "Nouvelle Municipalite X"},
                    new MunicipaliteDTO {mcode = 202, munnom = "Nouvelle Municipalite Y"},
                    new MunicipaliteDTO {mcode = 203, munnom = "Nouvelle Municipalite Z"}
                };

                List<MunicipaliteDTO> municipalitesExistantes = new List<MunicipaliteDTO>();

                mockDepotImportation.Setup(d => d.LireMunicipalite()).Returns(municipalitesImportees);
                mockDepotMunicipalites.Setup(d => d.listerMunicipalitesActives()).Returns(municipalitesExistantes);

                StatistiquesImportationDonnees resultat = service.Executer();

                Assert.Equal(0, resultat.NombreEnregistrementsDesactives);
                Assert.Equal(3, resultat.NombreEnregistrementsAjoutes);
                Assert.Equal(0, resultat.NombreEnregistrementsModifies);
                mockDepotMunicipalites.Verify(d => d.AjouterMunicipalite(It.IsAny<MunicipaliteDTO>()), Times.Exactly(3));
            }
        }
    }
}