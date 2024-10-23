CREATE DATABASE municipalites;
GO

USE municipalites;
GO

CREATE TABLE municipalites 
(
    MunicipaliteId INT IDENTITY(1,1) PRIMARY KEY,
    CodeGeographique INT NOT NULL,
    Nom NVARCHAR(255) NOT NULL,
    AdresseCourriel NVARCHAR(255),
    AdresseWeb NVARCHAR(255),
    DateConstruction DATE,
    Superficie FLOAT,
    Population INT,
    Actif BIT NOT NULL DEFAULT 1
);
GO

EXEC sp_rename 'municipalites.CodeGeographique', 'mcode', 'COLUMN';
EXEC sp_rename 'municipalites.Nom', 'munnom', 'COLUMN';
EXEC sp_rename 'municipalites.AdresseCourriel', 'mcourriel', 'COLUMN';
EXEC sp_rename 'municipalites.AdresseWeb', 'mweb', 'COLUMN';
EXEC sp_rename 'municipalites.DateConstruction', 'mdatcons', 'COLUMN';
EXEC sp_rename 'municipalites.Population', 'mpopul', 'COLUMN';
EXEC sp_rename 'municipalites.Superficie', 'msuperf', 'COLUMN';
ALTER TABLE municipalites DROP COLUMN MunicipaliteId;
ALTER TABLE municipalites
ADD CONSTRAINT PK_municipalites PRIMARY KEY (mcode);
GO

SELECT * FROM municipalites;
GO